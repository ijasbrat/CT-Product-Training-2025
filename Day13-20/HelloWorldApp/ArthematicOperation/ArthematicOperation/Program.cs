using System;

namespace ArthematicOperation
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Enetr your name:");
            string name = Console.ReadLine();
            Console.WriteLine("we are going to perform arthematic operations now:");
            Console.Write("Enter your first operand:");
            var first = Console.ReadLine();
            int a = Convert.ToInt32(first);
            // its is used to convert because whatever we may write it will store as string..
            Console.Write("enter your second operand:");
            var second = Console.ReadLine();
            int b = Convert.ToInt32(second);
            Console.WriteLine("___Which Operation do you wanna do now please select the option......");
            Console.WriteLine("1.addition");
            Console.WriteLine("2.subraction");
            Console.WriteLine("3.multiplication");
            Console.WriteLine("4.division");
            Console.WriteLine("5.remainder");
            Console.WriteLine("6.exit");
            Console.Write("your option:");
            var option = Console.ReadLine();
            int op = Convert.ToInt32(option);

            switch (op)
            {
                case 1:
                    int result = a + b;
                    Console.Write("the result is :");
                    Console.WriteLine(result);
                    break;
                case 2:
                    int output = a - b;
                    Console.Write("the result is :");
                    Console.WriteLine(output);
                    break;
                case 3:
                    int outcome = a * b;
                    Console.Write("the result is :");
                    Console.WriteLine(outcome);
                    break;
                case 4:
                    if (b != 0)
                    {
                        int damn = a / b;
                        Console.Write("the result is :");
                        Console.WriteLine(damn);
                    }
                    else
                    {
                        Console.WriteLine("the second number should not be 'zero'");
                    }
                    break;
                case 5:
                    int wow = a % b;
                    Console.Write("the result is :");
                    Console.WriteLine(wow);
                    break;
                case 6:
                    Console.WriteLine("thank you for performing tasks you have exited the process...");
                    break;
                default:
                    Console.WriteLine("enter correct options");
                    break;
            }
            // after completion we have to press enter to move to next process
            Console.WriteLine("shall we continue the journey (press enter to continue)...");
            Console.ReadLine();
            Console.WriteLine("we will now evaluate your grade...");
            Console.Write("enter your mark  with point value(range between 0 to 100):");
            float marks = Console.Read();
            if (marks < 50)
            {
                Console.WriteLine($"{name} failed you idiot");
            }
            //here i am used logical operator
            else if(marks == 50)
            {
                Console.WriteLine($"{name} you escaped with 'D' grade");
            }
            else if (marks >= 51 && marks <= 60)
            {
                Console.WriteLine($"{name} you passed with 'C' grade");
            }
            else if (marks > 60 && marks <= 70)
            {
                Console.WriteLine($"{name} you passed with 'B' grade");
            }
            else if (marks > 70 && marks <= 80)
            {
                Console.WriteLine($"{name} you passed with 'A' grade");
            }
            else if (marks > 80 && marks <= 90)
            {
                Console.WriteLine($"{name} you passed with 'S' grade");
            }
            else if (marks > 90 && marks <= 100)
            {
                Console.WriteLine($"{name} you passed with distinction 'O' grade");
            }
            // here i have used || logical operator
            else if (marks == 0 || marks == -marks )
            {
                Console.WriteLine($"{name}  please enter valid value" );
            }
            else
            {
                Console.WriteLine("choose correct number fella");
            }
        }
    }
}
