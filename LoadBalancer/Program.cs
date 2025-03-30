using System;
using System.Net;

namespace LoadBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress host = IPAddress.Parse("127.0.0.1");

            Task.Run(() => LoadBalancer.HealthChecker());

            LoadBalancer.StartLoadBalancer(host);
        }
    }
}