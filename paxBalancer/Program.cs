using System;
using System.Collections.Generic;
using System.Diagnostics;
using Constant;
class Program
{
    static void Main(string[] args)
    {
        string exePath = @"D:\Project\paxBalancerProj\ServerProject\bin\Debug\net8.0\ServerProject.exe";

       var instanceArgs = Constants.BackendServers.Select(a=>a.Item2).ToList();

        List<Process> processes = new List<Process>();

        foreach (var arg in instanceArgs)   
        {
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arg.ToString(),
                UseShellExecute = false,
                CreateNoWindow = false
            };

            try
            {
                var process = Process.Start(psi);
                processes.Add(process);
                Console.WriteLine($"Started process PID {process.Id} with argument: {arg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start process with argument '{arg}': {ex.Message}");
            }
        }

        Console.WriteLine("All processes started. Waiting for them to exit...");

        foreach (var proc in processes)
        {
            proc.WaitForExit();
            Console.WriteLine($"Process {proc.Id} exited with code {proc.ExitCode}");
        }

        Console.WriteLine("All processes have completed.");
    }
}
