using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int clientCount = 20;
            for (int i = 0; i < clientCount; i++)
            {
                IPAddress host = IPAddress.Parse("127.0.0.1");
                ClientSocket client = new ClientSocket(host, 6020);
                client.SendRequest();
                Thread.Sleep(2000);
            }
        }
    }
}