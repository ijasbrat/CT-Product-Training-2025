using System;
using System.IO;

public interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}
public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        Console.ResetColor();
    }

    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARNING] {DateTime.Now}: {message}");
        Console.ResetColor();
    }
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
        Console.ResetColor();
    }
}
public class FileLogger : ILogger
{
    private readonly string logFilePath = "log.txt";

    public void LogInfo(string message)
    {
        LogToFile("INFO", message);
    }

    public void LogWarning(string message)
    {
        LogToFile("WARNING", message);
    }

    public void LogError(string message)
    {
        LogToFile("ERROR", message);
    }

    private void LogToFile(string level, string message)
    {
        string log = $"[{level}] {DateTime.Now}: {message}";
        File.AppendAllText(logFilePath, log + Environment.NewLine);
    }
}
public class AppService
{
    private readonly ILogger _logger;
    public AppService(ILogger logger)
    {
        _logger = logger;
    }
    public void Run()
    {
        _logger.LogInfo("Application started successfully.");
        _logger.LogWarning("Low disk space detected.");
        _logger.LogError("An unexpected error occurred!");
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine(" Logging Application ");
        Console.WriteLine("Choose Logger Type:");
        Console.WriteLine("1. Console Logger");
        Console.WriteLine("2. File Logger");
        Console.Write("Enter choice (1 or 2): ");
        string choice = Console.ReadLine();
        ILogger logger;
        if (choice == "2")
            logger = new FileLogger();
        else
            logger = new ConsoleLogger();

        AppService app = new AppService(logger);
        app.Run();

        Console.WriteLine("\n Logging complete. Check console or 'log.txt' depending on your choice.");
    }
}
