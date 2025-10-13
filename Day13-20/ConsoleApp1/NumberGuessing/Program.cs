using System;

namespace NumberGuessingGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I have chosen a number between 1 and 100.");
            Console.WriteLine("Try to guess it!");
            Console.WriteLine();
            Random random = new Random();
            int target = random.Next(1, 101); 
            int guess = 0;                    
            int attempts = 0;                 
            while (guess != target)
            {
                Console.Write("Enter your guess (1–100): ");
                string input = Console.ReadLine()
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine(" Please enter a valid number!");
                    continue; 
                }
                attempts++;
                if (guess < target)
                {
                    Console.WriteLine("Too low! Try again.\n");
                }
                else if (guess > target)
                {
                    Console.WriteLine("Too high! Try again.\n");
                }
                else
                {
                    
                    Console.WriteLine($"\n Congratulations! You guessed the number {target} correctly!");
                    Console.WriteLine($"It took you {attempts} attempts.");
                }
            }
            Console.WriteLine("\nThanks for playing! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
