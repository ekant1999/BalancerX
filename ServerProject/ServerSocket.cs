using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServerSocket
{
    public static void RunServerSession(IPAddress host, int port)
    {
        TcpListener listener = new TcpListener(host, port);
        listener.Start();
        Console.WriteLine($"[BACKEND] Running on {host}:{port}");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine($"[BACKEND] Accepted connection");

            HandleClient(client, port);
        }
    }

    private static void HandleClient(TcpClient client, int port)
    {
        using (client)
        using (NetworkStream stream = client.GetStream())
        {
            byte[] buffer = new byte[4096];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine("[BACKEND] Request:");
            Console.WriteLine(request);

            if (request.Contains("/health"))
            {
                HandleHealthCheck(stream);
            }
            else
            {
                RespondWithHello(stream, port);
            }
        }
    }

    private static void HandleHealthCheck(NetworkStream stream)
    {
        Random rand = new Random();
        int bit = rand.Next(0, 3);

        if (bit == 1)
        {
            SendResponse(stream, "HTTP/1.1 503 Service Unavailable\r\nContent-Length: 0\r\n\r\n");
            Thread.Sleep(3000);
        }
        else
        {
            SendResponse(stream, "HTTP/1.1 200 OK\r\nContent-Length: 0\r\n\r\n");
        }
    }

    private static void RespondWithHello(NetworkStream stream, int port)
    {
        string body = $"Hello from backend on port {port}";
        string response = $"HTTP/1.1 200 OK\r\n\r\n{body}";
        SendResponse(stream, response);
    }

    private static void SendResponse(NetworkStream stream, string response)
    {
        byte[] responseBytes = Encoding.ASCII.GetBytes(response);
        stream.Write(responseBytes, 0, responseBytes.Length);
    }
}
