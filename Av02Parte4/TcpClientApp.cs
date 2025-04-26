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

                var recieveMessageTask = Task.Run(() => RecieveMessageAsync());
            }
            catch (Exception ex)
            {
                _form.AddMessage($"Erro: {ex.Message}");
            }
        }
        private async Task RecieveMessageAsync()
        {
            try
            {
                byte[] buffer = new byte[8192];

                while (true)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Conexão encerrada pelo servidor.");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (message.StartsWith("[CONNUSERS]"))
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
                        _form.AddMessage($"{message}");
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
