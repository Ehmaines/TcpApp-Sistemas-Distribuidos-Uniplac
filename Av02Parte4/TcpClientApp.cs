using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Av02Parte4
{
    public class TcpClientApp
    {
        private readonly string _ipServer;
        private readonly int _port;
        private NetworkStream _stream;
        private TcpClient _client;
        private readonly Form1 _form;

        public TcpClientApp(string ipServer, int port, Form1 form)
        {
            _ipServer = ipServer;
            _port = port;
            _form = form;
        }

        public async Task StartAsync()
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(_ipServer, _port);

                _stream = _client.GetStream();

                var recieveMessageTask = Task.Run(() => ReceiveMessageAsync());
            }
            catch (Exception ex)
            {
                _form.AddMessage($"Erro: {ex.Message}");
            }
        }
        private async Task ReceiveMessageAsync()
        {
            try
            {
                byte[] buffer = new byte[8192];

                bool isWaitingFile = false;
                int remainingFileBytes = 0;
                string fileName = "";
                List<byte> fileBuffer = new List<byte>();

                while (true)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Conexão encerrada pelo servidor.");
                        break;
                    }

                    if (!isWaitingFile)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (message.StartsWith("[FILESEND]"))
                        {
                            // Arquivo chegando
                            var parts = message.Split(':', 3);
                            if (parts.Length >= 3)
                            {
                                fileName = parts[1];
                                remainingFileBytes = int.Parse(parts[2]);
                                isWaitingFile = true;
                                fileBuffer = new List<byte>();
                            }
                        }
                        else if (message.StartsWith("[CONNUSERS]"))
                        {
                            message = message.Substring(12).Trim();
                            _form.AddMessage($"Usuários Conectados: {message}");
                        }
                        else if (message.StartsWith("[RELOADUSERLIST]"))
                        {
                            var users = message.Substring(16).Trim();
                            var connectedUsers = users.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                            _form.ReloadUsers(connectedUsers);
                        }
                        else
                        {
                            _form.AddMessage($"{message}");
                        }
                    }
                    else
                    {
                        // Modo recebendo arquivo
                        fileBuffer.AddRange(buffer.Take(bytesRead));
                        remainingFileBytes -= bytesRead;

                        if (remainingFileBytes <= 0)
                        {
                            await _form.SaveReceivedFileAsync(fileName, fileBuffer.ToArray());

                            isWaitingFile = false;
                            fileName = "";
                            remainingFileBytes = 0;
                            fileBuffer.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _form.AddMessage($"Erro: {ex.Message}");
            }
        }
        public async Task SendMessageAsync(string message)
        {
            if (_stream == null || !_client.Connected)
            {
                _form.AddMessage("Conexão não estabelecida.");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                _form.AddMessage($"Erro: {ex.Message}");
            }
        }

        public async Task SendFileAsync(string filePath)
        {
            if (_client == null || !_client.Connected)
            {
                MessageBox.Show("Client is not connected to the server.");
                return;
            }

            NetworkStream stream = _client.GetStream();

            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            long fileSize = fileInfo.Length;

            string command = $"/sendfile:{fileName}:{fileSize}";//Coloquei : aqui pois não pode ser usado no nome de arquivo, ai fica mais facil fazer o split no server
            byte[] commandBytes = Encoding.UTF8.GetBytes(command);
            await stream.WriteAsync(commandBytes, 0, commandBytes.Length);

            await Task.Delay(100); //Precisa desse delay para não enviar o arquivo antes do comando

            byte[] fileBytes = File.ReadAllBytes(filePath);
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);

            MessageBox.Show($"File {fileName} sent successfully!");
        }

        public async Task SendFileWhisperAsync(string filePath, string namesToSendFile)
        {
            if (_client == null || !_client.Connected)
            {
                MessageBox.Show("Client is not connected to the server.");
                return;
            }

            NetworkStream stream = _client.GetStream();

            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            long fileSize = fileInfo.Length;

            string command = $"/sendfilewhisper:{fileName}:{fileSize}:{namesToSendFile}";
            byte[] commandBytes = Encoding.UTF8.GetBytes(command);
            await stream.WriteAsync(commandBytes, 0, commandBytes.Length);

            await Task.Delay(100); //Precisa desse delay para não enviar o arquivo antes do comando

            byte[] fileBytes = File.ReadAllBytes(filePath);
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);

            MessageBox.Show($"File {fileName} sent successfully!");
        }

        public async Task SetClientName(string name)
        {
            if (_stream == null || !_client.Connected)
            {
                _form.AddMessage("Conexão não estabelecida.");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(name);
                await _stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                _form.AddMessage($"Erro: {ex.Message}");
            }
        }
    }
}
