using System;

namespace ECommerceApp
{
    public class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;

        public Product(int id, string name, double price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void DisplayProduct()
        {
            Console.WriteLine("ID: {0} | {1} | Price: {2:C2} | In stock: {3}", Id, Name, Price, Quantity);
        }
        public Product Clone()
        {
            return new Product(Id, Name, Price, Quantity);
        }
    }
    public class CartItem
    {
        public Product Product;
        public int Quantity;

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
    public class ShoppingCart
    {
        private CartItem[] items;
        private int count;

        public ShoppingCart(int initialCapacity)
        {
            if (initialCapacity < 1) initialCapacity = 4;
            items = new CartItem[initialCapacity];
            count = 0;
        }
        private void EnsureCapacity()
        {
            if (count < items.Length) return;

            int newSize = items.Length * 2;
            CartItem[] newItems = new CartItem[newSize];
            for (int i = 0; i < items.Length; i++)
                newItems[i] = items[i];

            items = newItems;
        }
        private int FindIndexByProductId(int productId)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Product.Id == productId) return i;
            }
            return -1;
        }
        public void AddProduct(Product product, int quantityToAdd)
        {
            if (product == null) throw new ArgumentNullException("product");
            if (quantityToAdd <= 0) throw new ArgumentException("Quantity must be positive.");

            int idx = FindIndexByProductId(product.Id);
            if (idx >= 0)
            {
                items[idx].Quantity += quantityToAdd;
            }
            else
            {
                EnsureCapacity();
                Product cloned = product.Clone();
                items[count] = new CartItem(cloned, quantityToAdd);
                count++;
            }
        }
        public bool RemoveProduct(int productId)
        {
            int idx = FindIndexByProductId(productId);
            if (idx == -1) return false;
            for (int i = idx; i < count - 1; i++)
                items[i] = items[i + 1];

            items[count - 1] = null;
            count--;
            return true;
        }
        public void DisplayCart()
        {
            if (count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine("Your Shopping Cart:");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("ID | Product                 | Qty | Unit Price | Subtotal");
            Console.WriteLine("------------------------------------------------------");

            for (int i = 0; i < count; i++)
            {
                CartItem ci = items[i];
                double subtotal = ci.Quantity * ci.Product.Price;
                Console.WriteLine("{0,2} | {1,-22} | {2,3} | {3,10:C2} | {4,8:C2}",
                    ci.Product.Id, ci.Product.Name, ci.Quantity, ci.Product.Price, subtotal);
            }

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Total: {0:C2}", CalculateTotal());
        }
        public double CalculateTotal()
        {
            double total = 0.0;
            for (int i = 0; i < count; i++)
            {
                total += items[i].Quantity * items[i].Product.Price;
            }
            return total;
        }

        public bool IsEmpty()
        {
            return count == 0;
        }
        public CartItem[] GetItemsCopy()
        {
            CartItem[] copy = new CartItem[count];
            for (int i = 0; i < count; i++) copy[i] = items[i];
            return copy;
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++) items[i] = null;
            count = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Product[] catalog = new Product[5];
            catalog[0] = new Product(1, "books", 499.00, 10);
            catalog[1] = new Product(2, "charger", 899.50, 8);
            catalog[2] = new Product(3, "mug", 2499.99, 5);
            catalog[3] = new Product(4, "chair", 8999.00, 3);
            catalog[4] = new Product(5, "stand", 1299.00, 12);

            ShoppingCart cart = new ShoppingCart(4);

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine("\nE-Commerce App (no generics, no LINQ)");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Remove Product from Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");

                string choiceInput = Console.ReadLine();
                int choice;
                if (!int.TryParse(choiceInput, out choice))
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
                            for (int i = 0; i < catalog.Length; i++)
                                catalog[i].DisplayProduct();
                            break;

                        case 2:
                            Console.Write("Enter the Product ID to add: ");
                            string idInput = Console.ReadLine();
                            int addId;
                            if (!int.TryParse(idInput, out addId))
                            {
                                Console.WriteLine("Invalid product ID. Please enter an integer.");
                                break;
                            }

                            Product productToAdd = null;
                            for (int i = 0; i < catalog.Length; i++)
                            {
                                if (catalog[i].Id == addId)
                                {
                                    productToAdd = catalog[i];
                                    break;
                                }
                            }

                            if (productToAdd == null)
                            {
                                Console.WriteLine("No product found with ID {0}.", addId);
                                break;
                            }

                            Console.Write("Enter quantity to add (Available: {0}): ", productToAdd.Quantity);
                            string qtyInput = Console.ReadLine();
                            int qty;
                            if (!int.TryParse(qtyInput, out qty) || qty <= 0)
                            {
                                Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                                break;
                            }
                            int currentInCart = 0;
                            CartItem[] snapshot = cart.GetItemsCopy();
                            for (int i = 0; i < snapshot.Length; i++)
                            {
                                if (snapshot[i].Product.Id == addId)
                                {
                                    currentInCart = snapshot[i].Quantity;
                                    break;
                                }
                            }

                            if (qty + currentInCart > productToAdd.Quantity)
                            {
                                Console.WriteLine("Cannot add {0} item(s). Only {1} item(s) can still be added based on stock.",
                                    qty, productToAdd.Quantity - currentInCart);
                                break;
                            }

                            cart.AddProduct(productToAdd, qty);
                            Console.WriteLine("{0} x '{1}' added to cart.", qty, productToAdd.Name);
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
                            int removeId;
                            if (!int.TryParse(removeInput, out removeId))
                            {
                                Console.WriteLine("Invalid product ID. Please enter an integer.");
                                break;
                            }

                            bool removed = cart.RemoveProduct(removeId);
                            if (removed)
                                Console.WriteLine("Product with ID {0} removed from cart.", removeId);
                            else
                                Console.WriteLine("Product with ID {0} is not in the cart.", removeId);
                            break;

                        case 5:
                            if (cart.IsEmpty())
                            {
                                Console.WriteLine("Your cart is empty. Add items before checkout.");
                                break;
                            }

                            cart.DisplayCart();
                            Console.Write("Proceed to checkout? (yes/no): ");
                            string confirm = Console.ReadLine();
                            if (confirm == null) confirm = "";
                            confirm = confirm.Trim().ToLower();

                            if (confirm == "yes" || confirm == "y")
                            {
                                CartItem[] cartItems = cart.GetItemsCopy();
                                bool stockOk = true;

                                for (int i = 0; i < cartItems.Length; i++)
                                {
                                    CartItem ci = cartItems[i];
                                    Product catalogProduct = null;
                                    for (int j = 0; j < catalog.Length; j++)
                                    {
                                        if (catalog[j].Id == ci.Product.Id)
                                        {
                                            catalogProduct = catalog[j];
                                            break;
                                        }
                                    }

                                    if (catalogProduct == null || ci.Quantity > catalogProduct.Quantity)
                                    {
                                        stockOk = false;
                                        Console.WriteLine("Insufficient stock for '{0}'. Available: {1}",
                                            ci.Product.Name, catalogProduct == null ? 0 : catalogProduct.Quantity);
                                    }
                                }
                                if (!stockOk)
                                {
                                    Console.WriteLine("Checkout failed due to stock issues. Please adjust your cart.");
                                    break;
                                }
                                for (int i = 0; i < cartItems.Length; i++)
                                {
                                    CartItem ci = cartItems[i];
                                    for (int j = 0; j < catalog.Length; j++)
                                    {
                                        if (catalog[j].Id == ci.Product.Id)
                                        {
                                            catalog[j].Quantity -= ci.Quantity;
                                            break;
                                        }
                                    }
                                }

                                double total = cart.CalculateTotal();
                                cart.Clear();
                                Console.WriteLine("Checkout successful! Grand Total: {0:C2}", total);
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
                    Console.WriteLine("An error occurred: {0}", ex.Message);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}