using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQAdvancedDemo
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
            Console.WriteLine("=== Advanced LINQ Operations ===");
            List<Product> products = new List<Product>
            {
                new Product("Laptop", "Electronics", 75000),
                new Product("Smartphone", "Electronics", 35000),
                new Product("Headphones", "Electronics", 5000),
                new Product("Shirt", "Clothing", 1500),
                new Product("Jeans", "Clothing", 2500),
                new Product("Book", "Stationery", 600),
                new Product("Pen", "Stationery", 50),
                new Product("Notebook", "Stationery", 120),
                new Product("Tablet", "Electronics", 28000)
            };
            var groupByCategory =
                from p in products
                group p by p.Category into g
                orderby g.Count() descending
                select new
                {
                    Category = g.Key,
                    Count = g.Count(),
                    AveragePrice = g.Average(x => x.Price)
                }; 
            Console.WriteLine("\nProducts Grouped by Category:");
            foreach (var group in groupByCategory)
            {
                Console.WriteLine($"Category: {group.Category}");
                Console.WriteLine($" → Number of Products: {group.Count}");
                Console.WriteLine($" → Average Price: ₹{group.AveragePrice:F2}");
                Console.WriteLine();
            }
            Console.WriteLine(" Advanced LINQ demo completed.");
        }
    }
}
