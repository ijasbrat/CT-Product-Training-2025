using System;
using System.Timers;
public class Clock
{
    public event EventHandler OnTick;

    private System.Timers.Timer timer;

    public Clock()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += TimerElapsed;
    }
    public void Start()
    {
        timer.Start();
    }
    public void Stop()
    {
        timer.Stop();
    }
    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        OnTick?.Invoke(this, EventArgs.Empty);
    }
}
public class Display
{
    public void ShowTime(object sender, EventArgs e)
    {
        Console.WriteLine($"Current Time: {DateTime.Now:HH:mm:ss}");
    }
}
class Program
{
    static void Main()
    {
        Clock clock = new Clock();
        Display display = new Display();
        clock.OnTick += display.ShowTime;
        Console.WriteLine("Clock started! Press ENTER to stop...\n");
        clock.Start();
        Console.ReadLine();
        clock.Stop();
        Console.WriteLine("\nClock stopped. Goodbye!");
    }
}
