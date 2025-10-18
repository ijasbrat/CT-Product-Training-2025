using System;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace RandomNumber
{
    class Progam
    {
        static void Main()
        {
            Console.Write("please enter your name:");
            var name = Console.ReadLine();
            Console.WriteLine($"welcome to number guessing game {name}.....");
            Console.WriteLine("shall we start the game ?.. (press yes,y or no,n):");
            var answer = Console.ReadLine();
            if (answer == "yes" || answer == "y")
            { 
                Random random = new Random();
                int target = random.Next(1, 101);

                Console.WriteLine("now i have picked up some number on my own lets see if you can find it or not.....");
                int guess = 0; 
                int attempts = 0;
                while (guess!=target)
                {
                    Console.Write("enter your guess:");
                    guess=Convert.ToInt32(Console.ReadLine());
                    attempts++;
                    if (guess > target)
                    {
                        Console.WriteLine($"{name} your guess is high");
                    }
                    else if (guess < target)
                    {
                        Console.WriteLine($"{name} your guess is low");
                    }
                    else
                    {
                        Console.WriteLine($"your guess is correct this is my guessed number :{guess}");
                        Console.WriteLine($"you have completed at {attempts} time");
                    }
                }
            }
            else
            {
                Console.WriteLine("okay! lets try again some other time , bye bye.....");
            }
        }
    }
}