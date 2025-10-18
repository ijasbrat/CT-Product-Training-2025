using System;

namespace ExceptionHandling
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("welcome to divison operation....");
                Console.Write("lets start with your name:");
                string name = Console.ReadLine();
                Console.Write("Enter your first operand:");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter your second operand:");
                int b = Convert.ToInt32(Console.ReadLine());
                int result = a / b;
                Console.WriteLine($"{name} your output is : {result}" );
            }
            catch(Exception ex)
            {
                Console.WriteLine($"unexpected error : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("thank you performing the operation bye byee....");
            }
            
        }
    }
}