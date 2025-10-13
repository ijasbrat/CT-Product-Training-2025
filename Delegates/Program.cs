using System;

namespace DelegatesDemo
{
    
    public delegate int MathOperation(int a, int b);

    internal class Program
    {
        
        public static int Add(int a, int b) => a + b;
        public static int Subtract(int a, int b) => a - b;
        public static int Multiply(int a, int b) => a * b;
        public static int Divide(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine("❌ Division by zero not allowed.");
                return 0;
            }
            return a / b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Delegate Demo ===");

            
            MathOperation op;

            op = Add;
            Console.WriteLine($"Addition: 10 + 5 = {op(10, 5)}");

            op = Subtract;
            Console.WriteLine($"Subtraction: 10 - 5 = {op(10, 5)}");

            op = Multiply;
            Console.WriteLine($"Multiplication: 10 * 5 = {op(10, 5)}");

            op = Divide;
            Console.WriteLine($"Division: 10 / 5 = {op(10, 5)}");

            Console.WriteLine("✅ Delegate demo completed.\n");
        }
    }
}
