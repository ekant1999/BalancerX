using System;
using System.Net;
using System.Net.Sockets;

namespace PaxBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress host = IPAddress.Parse("127.0.0.1");
            int port = Int32.Parse(args[0]);
            ServerSocket.RunServerSession(host, port);

        }
    }
}