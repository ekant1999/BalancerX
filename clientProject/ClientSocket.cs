using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ClientSocket
{
    private IPAddress host;
    private int port;

    public ClientSocket(IPAddress host, int port)
    {
        this.host = host;
        this.port = port;
    }

    public void SendRequest()
    {
        using (TcpClient client = new TcpClient())
        {
            client.Connect(host, port);
            Console.WriteLine($"[CLIENT] Connected to {host}:{port}");

            string httpRequest = "GET / HTTP/1.1\r\nHost: localHost.com\r\n\r\n";
            byte[] requestBytes = Encoding.ASCII.GetBytes(httpRequest);

            NetworkStream stream = client.GetStream();
            stream.Write(requestBytes, 0, requestBytes.Length);

            byte[] responseBuffer = new byte[4096];
            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);

            string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
            Console.WriteLine("[CLIENT] Response:");
            Console.WriteLine(response);
        }
    }
}
