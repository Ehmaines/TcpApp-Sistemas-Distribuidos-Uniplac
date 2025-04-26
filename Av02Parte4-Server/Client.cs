using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Av02Parte4_Server
{
    public class Client : TcpClient
    {
        public string Name { get; set; }
        public TcpClient client;
    }
}
