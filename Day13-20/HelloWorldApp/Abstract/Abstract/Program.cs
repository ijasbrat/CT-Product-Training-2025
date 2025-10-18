using System;
using System.Collections.Generic;
public abstract class Shape
{
    public abstract double GetArea();
    public abstract double GetPerimeter();
}
public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }
    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }
    public override double GetArea() => Length * Width;

    public override double GetPerimeter() => 2 * (Length + Width);
}

public class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(double a, double b, double c)
    {
        SideA = a;
        SideB = b;
        SideC = c;
    }
    public override double GetArea()
    {
        double s = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }
    public override double GetPerimeter() => SideA + SideB + SideC;
}
public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double GetArea() => Math.PI * Radius * Radius;

    public override double GetPerimeter() => 2 * Math.PI * Radius;
}

class Program
{
    static void Main()
    {
        List<Shape> shapes = new List<Shape>();
        bool addMore = true;

        Console.WriteLine(" Shape Calculator ");

        while (addMore)
        {
            Console.WriteLine("Choose a shape to add:");
            Console.WriteLine("1. Rectangle");
            Console.WriteLine("2. Triangle");
            Console.WriteLine("3. Circle");
            Console.Write("Enter choice (1-3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter length: ");
                    double length = double.Parse(Console.ReadLine());
                    Console.Write("Enter width: ");
                    double width = double.Parse(Console.ReadLine());
                    shapes.Add(new Rectangle(length, width));
                    break;
                case "2":
                    Console.Write("Enter side A: ");
                    double a = double.Parse(Console.ReadLine());
                    Console.Write("Enter side B: ");
                    double b = double.Parse(Console.ReadLine());
                    Console.Write("Enter side C: ");
                    double c = double.Parse(Console.ReadLine());
                    shapes.Add(new Triangle(a, b, c));
                    break;
                case "3":
                    Console.Write("Enter radius: ");
                    double radius = double.Parse(Console.ReadLine());
                    shapes.Add(new Circle(radius));
                    break;
                default:
                    Console.WriteLine(" Invalid choice. Try again.");
                    continue;
            }
            Console.Write("Do you want to add another shape? (y/n): ");
            string ans = Console.ReadLine().Trim().ToLower();
            addMore = ans == "y";
            Console.WriteLine();
        }
        double totalArea = 0;
        double totalPerimeter = 0;
        Console.WriteLine("\n Shape Details:\n");
        foreach (var shape in shapes)
        {
            string name = shape.GetType().Name;
            double area = shape.GetArea();
            double perimeter = shape.GetPerimeter();
            Console.WriteLine($"Shape: {name}");
            Console.WriteLine($"   Area: {area:F2}");
            Console.WriteLine($"   Perimeter: {perimeter:F2}\n");
            totalArea += area;
            totalPerimeter += perimeter;
        }
        Console.WriteLine($"Total Area of all shapes: {totalArea:F2}");
        Console.WriteLine($"Total Perimeter of all shapes: {totalPerimeter:F2}");
    }
}
