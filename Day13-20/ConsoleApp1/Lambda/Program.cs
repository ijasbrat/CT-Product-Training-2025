using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Lambda & LINQ Demo ");
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine("Even Numbers:");
            evenNumbers.ForEach(n => Console.WriteLine(n));
            Console.WriteLine(" Lambda demo completed.");
        }
    }
}
