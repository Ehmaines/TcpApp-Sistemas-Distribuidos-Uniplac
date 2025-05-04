using Av02Parte4_Server_UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Av02Parte4_Server
{
    public class TcpServer
    {
        private readonly int _port;
        private TcpListener _server;
        private List<Client> _clients = new List<Client>();
        private readonly FormServer _form;
        private readonly Logger _logger;

        public TcpServer(int port, FormServer form)
        {
            _port = port;
            _server = new TcpListener(IPAddress.Any, _port);
            _form = form;
            _logger = new Logger(_form);
        }

        public async Task StartAsync()
        {
            _server.Start();
            await _logger.LogAsync($"Servidor Iniciado na Porta {_port}");

            while (true)
            {
                TcpClient client = await _server.AcceptTcpClientAsync();
                await _logger.LogAsync($"{client.Client.RemoteEndPoint}; Cliente Conectado");
                var recieveMessage = Task.Run(() => ReceiveMessageFromClientAsync(client));
            }
        }

        private async Task ReceiveMessageFromClientAsync(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[8192];
                int bytesRead;
                List<string> recipientFileNames = new List<string>();
                bool isWaitingFile = false;
                int remainingFileBytes = 0;
                string fileName = "";

                List<byte> fileBuffer = new List<byte>();

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    if (!isWaitingFile)
                    {
                        // Receiving text message
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (message.StartsWith("/sendfile:"))
                        {
                            var parts = message.Split(':', 3); //Coloquei : aqui pois : não pode ser usado em nome de arquivo, ai fica mais facil fazer o split
                            if (parts.Length >= 3)
                            {
                                fileName = parts[1];
                                remainingFileBytes = int.Parse(parts[2]);
                                isWaitingFile = true;
                                fileBuffer = new List<byte>();

                                await _logger.LogAsync($"Preparing to receive file {fileName} ({remainingFileBytes} bytes)");
                            }
                        }
                        else if(message.StartsWith("/sendfilewhisper:"))
                        {
                            var parts = message.Split(':', 4);
                            if (parts.Length >= 4)
                            {
                                fileName = parts[1];
                                remainingFileBytes = int.Parse(parts[2]);
                                recipientFileNames = parts[3].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                                isWaitingFile = true;
                                fileBuffer = new List<byte>();

                                await _logger.LogAsync($"Preparing to receive file {fileName} ({remainingFileBytes} bytes)");
                            }
                        }
                        else
                        {
                            // Process normal commands like /setname, /listusers, etc.
                            await ProcessTextMessageAsync(message, client);
                        }
                    }
                    else
                    {
                        // Receiving file bytes
                        fileBuffer.AddRange(buffer.Take(bytesRead));
                        remainingFileBytes -= bytesRead;
                        if (remainingFileBytes <= 0)
                        {
                            await ProcessFileAsync(client, fileName, fileBuffer, recipientFileNames);

                            // Reset state
                            isWaitingFile = false;
                            fileName = "";
                            remainingFileBytes = 0;
                            fileBuffer.Clear();
                        }
                    }
                }

                ResetAllConnectedClients();
                await _logger.LogAsync($"{client.Client.RemoteEndPoint}; Cliente Desconectado");
            }
        }

        private async Task ProcessFileAsync(TcpClient senderClient, string fileName, List<byte> fileContent, List<string> recipientFileNames)
        {
            List<Client> clientsToSend = new List<Client>();
            if (recipientFileNames.Count == 0)
            {
                clientsToSend.AddRange(_clients);
            }
            else
            {
                clientsToSend = _clients.Where(c => recipientFileNames.Contains(c.Name, StringComparer.OrdinalIgnoreCase)).ToList();
            }
            await _logger.LogAsync(senderClient.Client.RemoteEndPoint.ToString(), _clients.FirstOrDefault(c => c.client == senderClient)!.Name!,  "arq", clientsToSend, fileName);

            foreach (var client in clientsToSend)
            {
                if (client.client != senderClient)
                {
                    NetworkStream clientStream = client.client.GetStream();

                    string notification = $"[FILESEND]:{fileName}:{fileContent.Count}";
                    byte[] notificationBytes = Encoding.UTF8.GetBytes(notification);
                    await clientStream.WriteAsync(notificationBytes, 0, notificationBytes.Length);

                    await Task.Delay(100); // Precisa desse delay para não enviar o arquivo antes do comando 

                    // Then send the file content
                    await clientStream.WriteAsync(fileContent.ToArray(), 0, fileContent.Count);
                }
            }
        }

        private async Task ProcessTextMessageAsync(string message, TcpClient client)
        {
            if (message.StartsWith("/setname "))
            {
                string name = message.Substring(9).Trim();
                var existClient = _clients.FirstOrDefault(c => c.client == client);
                
                if (existClient != default)
                {
                    existClient.Name = name;
                }
                else
                {
                    Client newClient = new Client { client = client, Name = name };
                    _clients.Add(newClient);
                }

                byte[] response = Encoding.UTF8.GetBytes($"[RELOADUSERLIST] {string.Join(", ", _clients.Select(c => c.Name))}");
                SendMessageToAll(response);

                ResetAllConnectedClients();
                await _logger.LogAsync(client.Client.RemoteEndPoint.ToString(), name, "NN");
            }
            else if (message.Equals("/listusers"))
            {
                var userList = string.Join(", ", _clients.Select(c => c.Name));
                byte[] response = Encoding.UTF8.GetBytes($"[CONNUSERS] {userList}");
                SendMessageToAll(response);
            }
            else if (message.Equals("/disconnect"))
            {
                var clientToRemove = _clients.FirstOrDefault(c => c.client == client);
                if (clientToRemove != null)
                {
                    await _logger.LogAsync(clientToRemove.client.Client.RemoteEndPoint.ToString(), clientToRemove.Name, "Logoff");
                    clientToRemove.client.Close();
                    _clients.Remove(clientToRemove);
                    var userList = string.Join(", ", _clients.Select(c => c.Name));

                    byte[] response = Encoding.UTF8.GetBytes($"[RELOADUSERLIST] {userList}");
                    SendMessageToAll(response);

                    ResetAllConnectedClients();
                }
            }
            else if (message.StartsWith("/whisper "))
            {
                string[] parts = message.Split(new[] { ' ' }, 3);
                if (parts.Length == 3)
                {
                    string recipientNames = parts[1];
                    string privateMessage = parts[2];

                    recipientNames = recipientNames.TrimEnd(',');
                    var recipientNameSplit = recipientNames.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<Client> recipientClients = new List<Client>();
                    foreach (var recipientName in recipientNameSplit)
                    {
                        var recipientClient = _clients.FirstOrDefault(c => c.Name.Equals(recipientName, StringComparison.OrdinalIgnoreCase));
                        if (recipientClient != null)
                        {
                            byte[] response = Encoding.UTF8.GetBytes($"{_clients.FirstOrDefault(c => c.client == client)?.Name} (Privado): {privateMessage}");
                            await recipientClient.client.GetStream().WriteAsync(response, 0, response.Length);
                            recipientClients.Add(recipientClient);
                        }
                    }
                    await _logger.LogAsync(client.Client.RemoteEndPoint.ToString(), _clients.FirstOrDefault(c => c.client == client)?.Name, "msg (whisper)", _clients, message);
                }
            }
            else
            {
                var name = _clients.FirstOrDefault(c => c.client == client)?.Name;
                await _logger.LogAsync(client.Client.RemoteEndPoint.ToString(), name!, "msg", _clients, message);
                foreach (var c in _clients)
                {
                    byte[] response = Encoding.UTF8.GetBytes($"{name}: {message}");
                    await c.client.GetStream().WriteAsync(response, 0, response.Length);
                }
            }
        }

        private async void SendMessageToAll(byte[] response)
        {
            foreach (var c in _clients)
            {
                await c.client.GetStream().WriteAsync(response, 0, response.Length);
            }
        }

        public void ResetAllConnectedClients()
        {
            List<string> connectedClients = new List<string>();

            foreach (var c in _clients)
            {
                connectedClients.Add(c.Name);
            }

            _form.ResetConnectedClients(connectedClients.ToArray());
        }
    }
}
