using System;
using System.Collections.Generic;

namespace GenericStackDemo
{
    
    public class Stack<T>
    {
        
        private List<T> elements = new List<T>();

        
        public void Push(T item)
        {
            elements.Add(item);
            Console.WriteLine($" Pushed: {item}");
        }
        public T Pop()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException(" Cannot pop from an empty stack.");

            
            T item = elements[elements.Count - 1];

            
            elements.RemoveAt(elements.Count - 1);

            Console.WriteLine($" Popped: {item}");
            return item;
        }

        
        public T Peek()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException(" Stack is empty — nothing to peek.");

            return elements[elements.Count - 1];
        }

        
        public void DisplayStack()
        {
            if (elements.Count == 0)
            {
                Console.WriteLine(" Stack is empty.");
                return;
            }

            Console.WriteLine(" Stack contents (top to bottom):");
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                Console.WriteLine($"  {elements[i]}");
            }
        }
    }

    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Stack<T>\n");

            
            Console.WriteLine(" Testing Stack<int>:");
            Stack<int> intStack = new Stack<int>();
            intStack.Push(10);
            intStack.Push(20);
            intStack.Push(30);
            intStack.DisplayStack();

            Console.WriteLine($"Top element (Peek): {intStack.Peek()}");
            intStack.Pop();
            intStack.DisplayStack();

            Console.WriteLine("\n-----------------------------\n");

            
            Console.WriteLine(" Testing Stack<string>:");
            Stack<string> stringStack = new Stack<string>();
            stringStack.Push("Alice");
            stringStack.Push("Bob");
            stringStack.Push("Charlie");
            stringStack.DisplayStack();

            Console.WriteLine($"Top element (Peek): {stringStack.Peek()}");
            stringStack.Pop();
            stringStack.DisplayStack();

            Console.WriteLine("\n Program completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
