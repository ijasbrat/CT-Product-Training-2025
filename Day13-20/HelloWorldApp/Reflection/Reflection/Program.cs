using System;
using System.Reflection;
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}
public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}
public class Serializer
{
    public static void SerializeObject(object obj)
    {
        if (obj == null)
        {
            Console.WriteLine("Null object provided.");
            return;
        }
        Type type = obj.GetType();
        Console.WriteLine($"\n--- {type.Name} Properties ---");
        PropertyInfo[] properties = type.GetProperties();
        if (properties.Length == 0)
        {
            Console.WriteLine("No public properties found.");
            return;
        }
        foreach (var prop in properties)
        {
            object value = prop.GetValue(obj, null);
            Console.WriteLine($"{prop.Name} = {value}");
        }
    }
}
class Program
{
    static void Main()
    {
        Person person = new Person
        {
            Name = "Alice",
            Age = 28,
            City = "New York"
        };
        Serializer.SerializeObject(person);
        Product product = new Product
        {
            Name = "Laptop",
            Category = "Electronics",
            Price = 75000
        };
        Serializer.SerializeObject(product);
        var anonymous = new { Title = "Book", Pages = 250, Author = "Bob" };
        Serializer.SerializeObject(anonymous);
    }
}
