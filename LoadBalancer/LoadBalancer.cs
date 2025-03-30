using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Constant;

namespace LoadBalancer
{
    class LoadBalancer
    {
        private static int roundRobinIndex = 0;
        private static readonly object LockObj = new object();
        private static List<(string, int)> healthyServers = Constants.BackendServers.ToList();
        private static readonly object HealthyServersLock = new object();
        private const string HealthPath = "/health";
        private const int HealthCheckTimeout = 2000; // ms
        private const int HealthCheckInterval = 5000; // ms

        public static void HealthChecker()
        {
            while (true)
            {
                var currentHealthy = new List<(string, int)>();

                foreach (var (host, port) in Constants.BackendServers)
                {
                    string url = $"http://{host}:{port}{HealthPath}";

                    try
                    {
                        var request = (HttpWebRequest)WebRequest.CreateHttp(url);
                        request.Timeout = HealthCheckTimeout;

                        using (var response = (HttpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                currentHealthy.Add((host, port));
                            }
                            else
                            {
                                Console.WriteLine($"[WARN] Server unhealthy: {host}:{port}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Health check failed for {host}:{port} - {ex.Message}");
                    }
                }

                lock (HealthyServersLock)
                {
                    healthyServers = currentHealthy;
                }

                Thread.Sleep(HealthCheckInterval);
            }
        }

        private static (string, int) GetNextServer()
        {
            lock (LockObj)
            {
                lock (HealthyServersLock)
                {
                    if (healthyServers.Count == 0)
                        throw new Exception("No healthy backend servers available");

                    roundRobinIndex %= healthyServers.Count;
                    var server = healthyServers[roundRobinIndex];
                    roundRobinIndex = (roundRobinIndex + 1) % healthyServers.Count;
                    return server;
                }
            }
        }

        public static void StartLoadBalancer(IPAddress host, int port = 6020)
        {
            TcpListener listener = new TcpListener(host, port);
            listener.Start();
            Console.WriteLine($"[INFO] Load Balancer running on {host}:{port}");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        private static void HandleClient(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream clientStream = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                    string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("[INFO] Received request:\n" + request);

                    var (backendHost, backendPort) = GetNextServer();
                    Console.WriteLine($"[INFO] Forwarding to {backendHost}:{backendPort}");

                    using (TcpClient backendClient = new TcpClient(backendHost, backendPort))
                    using (NetworkStream backendStream = backendClient.GetStream())
                    {
                        backendStream.Write(buffer, 0, bytesRead);

                        byte[] responseBuffer = new byte[4096];
                        int responseBytes = backendStream.Read(responseBuffer, 0, responseBuffer.Length);

                        clientStream.Write(responseBuffer, 0, responseBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Client handling failed: {ex.Message}");
            }
        }
    }
}
