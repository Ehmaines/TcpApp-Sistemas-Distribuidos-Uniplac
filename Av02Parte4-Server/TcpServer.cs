using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Av02Parte4_Server
{
    public class TcpServer
    {
        private readonly int _port;
        private TcpListener _server;
        private List<Client> _clients = new List<Client>();

        public TcpServer(int port)
        {
            _port = port;
            _server = new TcpListener(IPAddress.Any, _port);
        }

        public async Task StartAsync()
        {
            _server.Start();
            Console.WriteLine($"Servidor Iniciado na Porta {_port}");

            while (true)
            {
                TcpClient client = await _server.AcceptTcpClientAsync();
                Console.WriteLine("Cliente Conectado");
                var recieveMessage = Task.Run(() => RecieveMessageFromClientAsync(client));
            }
        }

        private async Task RecieveMessageFromClientAsync(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Mensagem Recebida: {message}");

                    if(message.StartsWith("/setname "))
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
                        Console.WriteLine($"Cliente {name} adicionado ou alterado o nome.");
                    }
                    else if (message.Equals("/listusers"))
                    {
                        var userList = string.Join(", ", _clients.Select(c => c.Name));
                        byte[] response = Encoding.UTF8.GetBytes($"[CONNUSERS] {userList}");
                        SendMessageToAll(response);
                    }
                    else if(message.Equals("/disconnect"))
                    {
                        var clientToRemove = _clients.FirstOrDefault(c => c.client == client);
                        if (clientToRemove != null)
                        {
                            clientToRemove.client.Close();
                            _clients.Remove(clientToRemove);
                            var userList = string.Join(", ", _clients.Select(c => c.Name));

                            byte[] response = Encoding.UTF8.GetBytes($"[RELOADUSERLIST] {userList}");
                            SendMessageToAll(response);

                            Console.WriteLine($"Cliente {clientToRemove.Name} desconectado.");
                        }
                        break;
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
                            var listToSendMessage = new List<string>();
                            foreach (var recipientName in recipientNameSplit)
                            {
                                var recipientClient = _clients.FirstOrDefault(c => c.Name.Equals(recipientName, StringComparison.OrdinalIgnoreCase));
                                
                                if (recipientClient != null)
                                {
                                    byte[] response = Encoding.UTF8.GetBytes($"{_clients.FirstOrDefault(c => c.client == client)?.Name} (Privado): {privateMessage}");
                                    await recipientClient.client.GetStream().WriteAsync(response, 0, response.Length);
                                }
                            }
                        }
                    }
                    else
                    {
                        var name = _clients.FirstOrDefault(c => c.client == client)?.Name;
                        // Enviar mensagem para todos os clientes conectados
                        foreach (var c in _clients)
                        {
                            if (c.client != client)
                            {
                                byte[] response = Encoding.UTF8.GetBytes($"{name}: {message}");
                                await c.client.GetStream().WriteAsync(response, 0, response.Length);
                            }
                        }
                    }
                    Console.WriteLine($"Mensagem Enviada: {message}");
                }

                Console.WriteLine("Cliente Desconectado");
            }
        }

        private async void SendMessageToAll(byte[] response)
        {
            foreach (var c in _clients)
            {
                await c.client.GetStream().WriteAsync(response, 0, response.Length);
            }
        }
    }
}
