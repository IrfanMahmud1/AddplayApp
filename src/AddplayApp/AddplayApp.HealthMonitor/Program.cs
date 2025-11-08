using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        var logPath = Path.Combine(projectRoot, "Logs", "testlog-.log");

        Directory.CreateDirectory(Path.GetDirectoryName(logPath));

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var ram = new PerformanceCounter("Memory", "Available MBytes");

            while (true)
            {
                Log.Information("Time: {Time}, CPU: {Cpu}%, Free RAM: {Ram} MB",
                    DateTime.Now, cpu.NextValue(), ram.NextValue());

                Thread.Sleep(10000);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while monitoring system health");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
