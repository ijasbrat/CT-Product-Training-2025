using System;
using System.Linq;

class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}

class Program
{
    static void Main()
    {
        System.Collections.Generic.List<Product> products = new System.Collections.Generic.List<Product>()
        {
            new Product { Name = "Laptop", Category = "Electronics", Price = 60000 },
            new Product { Name = "Mobile", Category = "Electronics", Price = 30000 },
            new Product { Name = "Headphones", Category = "Electronics", Price = 2000 },
            new Product { Name = "Table", Category = "Furniture", Price = 8000 },
            new Product { Name = "Chair", Category = "Furniture", Price = 4000 },
            new Product { Name = "Sofa", Category = "Furniture", Price = 15000 },
            new Product { Name = "T-shirt", Category = "Clothing", Price = 1200 },
            new Product { Name = "Jeans", Category = "Clothing", Price = 2500 },
            new Product { Name = "Jacket", Category = "Clothing", Price = 3500 }
        };
        var groupByCategory = from p in products
                              group p by p.Category into g
                              select new
                              {
                                  Category = g.Key,  
                                  Count = g.Count(),
                                  AveragePrice = g.Average(x => x.Price) 
                              };
        var orderedGroups = from g in groupByCategory
                            orderby g.Count descending
                            select g;
        Console.WriteLine("Products grouped by category (ordered by count):\n");
        foreach (var group in orderedGroups)
        {
            Console.WriteLine($"Category: {group.Category}");
            Console.WriteLine($"Product Count: {group.Count}");
            Console.WriteLine($"Average Price: ₹{group.AveragePrice}\n");
        }
    }
}
