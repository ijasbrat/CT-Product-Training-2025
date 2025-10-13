using System;

namespace ExceptionHandlingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Division with Exception Handling");
            Console.WriteLine();
            try
            {
                Console.Write("Enter the numerator: ");
                int numerator = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the denominator: ");
                int denominator = Convert.ToInt32(Console.ReadLine());
                double result = DivideNumbers(numerator, denominator);

                Console.WriteLine($"\nResult: {numerator} / {denominator} = {result}");
            }
            catch (FormatException)
            {
                
                Console.WriteLine("\n Error: Please enter valid integer values only.");
            }
            catch (DivideByZeroException)
            {
                
                Console.WriteLine("\n Error: Division by zero is not allowed!");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"\n Unexpected error: {ex.Message}");
            }
            finally
            {
                
                Console.WriteLine("\nProgram execution completed. Press any key to exit...");
                Console.ReadKey();
            }
        }
        static double DivideNumbers(int a, int b)
        { 
            return (double)a / b;
        }
    }
}
