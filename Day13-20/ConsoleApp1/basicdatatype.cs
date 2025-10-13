using System;

namespace BasicDataTypesWithInput
{
    internal class Program
    {
        static void Main(string[] args)
        {

        
            Console.Write("Enter an integer number: ");
            int number = Convert.ToInt32(Console.ReadLine()); 

          
            Console.Write("Enter a double value (e.g., 12.34): ");
            double price = Convert.ToDouble(Console.ReadLine()); 

            
            Console.Write("Is the item available? (true/false): ");
            bool isAvailable = Convert.ToBoolean(Console.ReadLine()); 

            
            Console.Write("Enter your grade (A/B/C/D): ");
            char grade = Convert.ToChar(Console.ReadLine());
            
            Console.Write("Enter your name: ");
            string name = Console.ReadLine(); 

            Console.WriteLine(); 

            
            Console.WriteLine("You Entered:");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Number: {number}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Is Available: {isAvailable}");
            Console.WriteLine($"Grade: {grade}");
            Console.WriteLine();

            
            int sum = number + 5;
            int difference = number - 3;
            int product = number * 2;
            double division = price / 2;

            Console.WriteLine("Arithmetic Operations:");
            Console.WriteLine($"Sum (number + 5): {sum}");
            Console.WriteLine($"Difference (number - 3): {difference}");
            Console.WriteLine($"Product (number * 2): {product}");
            Console.WriteLine($"Division (price / 2): {division}");
            Console.WriteLine();

            
            bool isGreater = number > 10;
            bool isEqual = grade == 'A';

            Console.WriteLine("Comparison and Logical Checks:");

            
            if (isGreater && isEqual)
            {
                Console.WriteLine("Number is greater than 10 AND grade is A.");
            }
            else if (isGreater || isAvailable)
            {
                Console.WriteLine("Either number is greater than 10 OR item is available.");
            }
            else
            {
                Console.WriteLine("Neither condition is true.");
            }

            Console.WriteLine();
            Console.WriteLine("Program completed successfully. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
