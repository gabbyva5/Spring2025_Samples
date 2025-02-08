using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item\n");

            Console.WriteLine("A. Add item to shopping cart");
            Console.WriteLine("L. List all items in shopping cart");
            Console.WriteLine("E. Remove item from shopping cart");
            Console.WriteLine("S. Update item quantity in shopping cart");
            Console.WriteLine("Q. Check out");

            List<Product?> list = ProductServiceProxy.Current.Products;
            Dictionary<int, int> cart= ShoppingCartService.Current.Cart;

            char choice;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.Write("Enter product name: ");
                        Product prod= ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        Console.Write("Enter product price (int): ");
                        prod.Price= int.Parse(Console.ReadLine() ?? "10");
                        Console.Write("Enter product quantity: ");
                        prod.Quantity= int.Parse(Console.ReadLine() ?? "1");
                        break;

                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;

                    case 'U':
                    case 'u':
                        //select one of the products
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p?.Id == selection);

                        if(selectedProd != null)
                        {
                            Console.Write("Enter new name: ");
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;

                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;

                    case 'A':
                    case 'a':   //add item to shopping cart
                        Console.WriteLine("Which product would you like to add to cart?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p?.Id == selection);
                        Console.WriteLine("How many do you want to add?");
                        int quan= int.Parse(Console.ReadLine() ?? "1"); //if invalid input, will set quantity to 1)

                        if(selectedProd != null)
                            ShoppingCartService.Current.Add(selectedProd, quan);
                        break;

                    case 'L':
                    case 'l':   //list all items in shopping cart
                        foreach(var item in cart)
                        {
                            Product? product= list.FirstOrDefault(p => p?.Id == item.Key);
                            if(product!=null)
                                Console.WriteLine($"{product.Id}. {product.Name} ({item.Value} in cart)\t ${product.Price} (each)");
                        }
                        break;

                    case 'E':
                    case 'e':   //remove item from shopping cart
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p?.Id == selection);
                        if(selectedProd!=null)
                            ShoppingCartService.Current.Delete(selectedProd);
                        break;
                    case 'S':
                    case 's':
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProd = list.FirstOrDefault(p => p?.Id == selection);
                        Console.WriteLine("How many total do you want (including amount already in cart)?");
                        quan= int.Parse(Console.ReadLine() ?? "1"); //if invalid input, will set quantity to 1)

                        if(selectedProd != null)
                            ShoppingCartService.Current.Update(selectedProd, quan);
                        break;

                    case 'Q':
                    case 'q':
                        if(cart.Count()!=0)
                        {
                            int total=0;
                            foreach (var items in cart)
                            {
                                int id= items.Key;
                                int quantity= items.Value;
                                Product? product= list.FirstOrDefault(p=>p?.Id== id);
                                if(product!=null)
                                {   
                                    Console.WriteLine("\n****************************************************************");
                                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Quantity: {quantity}, Total Price: {product.Price*quantity}");
                                    total+= product.Price*quantity;             
                                }
                            };
                            Console.WriteLine("****************************************************************");
                            Console.WriteLine($"Subtotal: {total}\nTotal: {total*1.07}");
                        }

                        else Console.WriteLine("You have no items in cart. No total.");
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');
        }
    }
}
