using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQBasicDemo
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public Product(string name, string category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LINQ Queries Demo ===");
            List<Product> products = new List<Product>
            {
                new Product("Laptop", "Electronics", 75000),
                new Product("Smartphone", "Electronics", 35000),
                new Product("Headphones", "Electronics", 5000),
                new Product("Shirt", "Clothing", 1500),
                new Product("Jeans", "Clothing", 2500),
                new Product("Book", "Stationery", 600),
                new Product("Pen", "Stationery", 50)
            };
            Console.Write("Enter category to filter (e.g., Electronics): ");
            string inputCategory = Console.ReadLine();
            var filteredProducts = from p in products
                                   where p.Category.Equals(inputCategory, StringComparison.OrdinalIgnoreCase)
                                   select p;
            Console.WriteLine($"\nProducts in category '{inputCategory}':");
            foreach (var p in filteredProducts)
            {
                Console.WriteLine($"- {p.Name} : ₹{p.Price}");
            }
            if (filteredProducts.Any())
            {
                double averagePrice = filteredProducts.Average(p => p.Price);
                Console.WriteLine($"\nAverage Price in '{inputCategory}' category: ₹{averagePrice:F2}");
            }
            else
            {
                Console.WriteLine("\nNo products found in this category.");
            }
            Console.WriteLine("\n✅ LINQ query completed.");
        }
    }
}
