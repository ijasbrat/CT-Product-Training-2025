using System;
using System.Timers.Timer;

namespace EventsDemo
{
    
    public class Clock
    {
        public event EventHandler OnTick; // Declare event

        private Timer timer;

        public Clock()
        {
            timer = new Timer(1000); // Trigger every 1 second
            timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Raise the event
            OnTick?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }

    // Step 2: Define Display class
    public class Display
    {
        public void Subscribe(Clock clock)
        {
            clock.OnTick += ShowTime;
        }

        private void ShowTime(object sender, EventArgs e)
        {
            Console.WriteLine($" Current Time: {DateTime.Now:T}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Event Demo (Clock) ===");

            Clock clock = new Clock();
            Display display = new Display();

            display.Subscribe(clock);
            clock.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            clock.Stop();
            Console.WriteLine("⏹ Clock stopped.");
        }
    }
}
