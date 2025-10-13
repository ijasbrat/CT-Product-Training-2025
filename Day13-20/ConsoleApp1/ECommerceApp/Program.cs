using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp
{
    public class Product
    {
        public int Id { get; set; }           
        public string Name { get; set; }      
        public double Price { get; set; }     
        public int Quantity { get; set; }
        public Product(int id, string name, double price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public void DisplayProduct()
        {
            Console.WriteLine($"ID: {Id} | {Name} | Price: {Price:C2} | In stock: {Quantity}");
        }
        public Product Clone()
        {
            return new Product(Id, Name, Price, Quantity);
        }
    }
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
    public class ShoppingCart
    { 
        private readonly List<CartItem> items = new List<CartItem>();
        public void AddProduct(Product product, int quantityToAdd)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantityToAdd <= 0) throw new ArgumentException("Quantity must be positive.");
            var existing = items.FirstOrDefault(ci => ci.Product.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantityToAdd;
            }
            else
            {   
                var clonedProduct = product.Clone();
                clonedProduct.Quantity = product.Quantity; 
                items.Add(new CartItem(clonedProduct, quantityToAdd));
            }
        }
        public bool RemoveProduct(int productId)
        {
            var existing = items.FirstOrDefault(ci => ci.Product.Id == productId);
            if (existing != null)
            {
                items.Remove(existing);
                return true;
            }
            return false;
        }
        public void DisplayCart()
        {
            if (!items.Any())
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }
            Console.WriteLine("Your Shopping Cart:");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("ID | Product                 | Qty | Unit Price | Subtotal");
            Console.WriteLine("----------------------------------------------");
            foreach (var ci in items)
            {
                double subtotal = ci.Quantity * ci.Product.Price;
                Console.WriteLine($"{ci.Product.Id,2} | {ci.Product.Name,-22} | {ci.Quantity,3} | {ci.Product.Price,10:C2} | {subtotal,8:C2}");
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Total: {CalculateTotal():C2}");
        }
        public double CalculateTotal()
        {
            return items.Sum(ci => ci.Quantity * ci.Product.Price);
        }
        public List<CartItem> GetCartItems()
        {
            
            return new List<CartItem>(items);
        }
        public void Clear()
        {
            items.Clear();
        } 
        public bool IsEmpty() => !items.Any();
    }

    class Program
    {
        static void Main(string[] args)
        {  
            List<Product> catalog = new List<Product>
            {
                new Product(1, "Wireless Mouse", 499.00, 10),
                new Product(2, "USB-C Charger", 899.50, 8),
                new Product(3, "Mechanical Keyboard", 2499.99, 5),
                new Product(4, "HD Monitor 24\"", 8999.00, 3),
                new Product(5, "Laptop Stand", 1299.00, 12)
            };
            ShoppingCart cart = new ShoppingCart();
            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.WriteLine("\n=== E-Commerce App ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Remove Product from Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");

                string choiceInput = Console.ReadLine();
                if (!int.TryParse(choiceInput, out int choice))
                {
                    Console.WriteLine("Invalid selection. Please enter a number between 1 and 6.");
                    continue; 
                }
                Console.WriteLine(); 
                try
                {
                    switch (choice)
                    {
                        case 1:
                            
                            Console.WriteLine("Product Catalog:");
                            foreach (var p in catalog)
                            {
                                p.DisplayProduct();
                            }
                            break;

                        case 2:
                            
                            Console.Write("Enter the Product ID to add: ");
                            string idInput = Console.ReadLine();
                            if (!int.TryParse(idInput, out int addId))
                            {
                                Console.WriteLine("Invalid product ID. Please enter a valid integer.");
                                break;
                            }

                            var productToAdd = catalog.FirstOrDefault(p => p.Id == addId);
                            if (productToAdd == null)
                            {
                                Console.WriteLine($"No product found with ID {addId}.");
                                break;
                            }

                            Console.Write($"Enter quantity to add (Available: {productToAdd.Quantity}): ");
                            string qtyInput = Console.ReadLine();
                            if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
                            {
                                Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                                break;
                            }
                            var currentInCart = cart.GetCartItems().FirstOrDefault(ci => ci.Product.Id == addId)?.Quantity ?? 0;
                            if (qty + currentInCart > productToAdd.Quantity)
                            {
                                Console.WriteLine($"Cannot add {qty} item(s). Only {productToAdd.Quantity - currentInCart} item(s) can still be added based on stock.");
                                break;
                            }
                            cart.AddProduct(productToAdd, qty);
                            Console.WriteLine($"{qty} x '{productToAdd.Name}' added to cart.");
                            break;
                        case 3:
                            cart.DisplayCart();
                            break;

                        case 4:
                            if (cart.IsEmpty())
                            {
                                Console.WriteLine("Your cart is empty. Nothing to remove.");
                                break;
                            }
                            Console.Write("Enter the Product ID to remove from the cart: ");
                            string removeInput = Console.ReadLine();
                            if (!int.TryParse(removeInput, out int removeId))
                            {
                                Console.WriteLine("Invalid product ID. Please enter a valid integer.");
                                break;
                            }

                            bool removed = cart.RemoveProduct(removeId);
                            if (removed)
                                Console.WriteLine($"Product with ID {removeId} removed from cart.");
                            else
                                Console.WriteLine($"Product with ID {removeId} is not in the cart.");
                            break;

                        case 5:
                            
                            if (cart.IsEmpty())
                            {
                                Console.WriteLine("Your cart is empty. Add items before checkout.");
                                break;
                            }
                            cart.DisplayCart();
                            Console.Write("Proceed to checkout? (yes/no): ");
                            string confirm = Console.ReadLine()?.Trim().ToLower();
                            if (confirm == "yes" || confirm == "y")
                            {
                                var cartItems = cart.GetCartItems();
                                bool stockOk = true;
                                foreach (var ci in cartItems)
                                {
                                    var catalogProduct = catalog.FirstOrDefault(p => p.Id == ci.Product.Id);
                                    if (catalogProduct == null || ci.Quantity > catalogProduct.Quantity)
                                    {
                                        stockOk = false;
                                        Console.WriteLine($"Insufficient stock for '{ci.Product.Name}'. Available: {catalogProduct?.Quantity ?? 0}");
                                    }
                                }
                                if (!stockOk)
                                {
                                    Console.WriteLine("Checkout failed due to stock issues. Please adjust your cart.");
                                    break;
                                }
                                foreach (var ci in cartItems)
                                {
                                    var catalogProduct = catalog.First(p => p.Id == ci.Product.Id);
                                    catalogProduct.Quantity -= ci.Quantity;
                                }
                                double total = cart.CalculateTotal();
                                cart.Clear();
                                Console.WriteLine($"Checkout successful! Grand Total: {total:C2}");
                            }
                            else
                            {
                                Console.WriteLine("Checkout canceled.");
                            }
                            break;

                        case 6:
                            
                            exitRequested = true;
                            Console.WriteLine("Thank you for visiting. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Please choose a valid option (1-6).");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
