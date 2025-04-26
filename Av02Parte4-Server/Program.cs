// See https://aka.ms/new-console-template for more information
using Av02Parte4_Server;

Console.WriteLine("Hello, World!");
var server = new TcpServer(8080);
await server.StartAsync();