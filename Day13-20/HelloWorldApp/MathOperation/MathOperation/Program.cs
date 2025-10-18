using System;

class Program
{
    public delegate int MathOperation(int a, int b);
    public static int Add(int a, int b)
    {
        return a + b;
    }
    public static int Subtract(int a, int b)
    {
        return a - b;
    }
    public static int Multiply(int a, int b)
    {
        return a * b;
    }
    public static int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero!");
        return a / b;
    }
    static void Main()
    {
        try
        {
            Console.Write("Enter first operand: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter second operand: ");
            int y = Convert.ToInt32(Console.ReadLine());
            MathOperation operation;
            operation = Add;
            Console.WriteLine($"the addition of two numbers is: {operation(x, y)}");
            operation = Subtract;
            Console.WriteLine($"the subtraction of two numbers is: {operation(x, y)}");
            operation = Multiply;
            Console.WriteLine($"the multiplication of two number is: {operation(x, y)}");
            operation = Divide;
            Console.WriteLine($"the division of two numbers is: {operation(x, y)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
