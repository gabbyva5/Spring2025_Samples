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
            Console.WriteLine("A. Add item to shopping cart ");
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
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
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
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;

                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;

                    case 'A':
                    case 'a':
                        break;
                    case 'E':
                    case 'e':
                        break;
                    case 'S':
                    case 's':
                        break;

                    case 'Q':
                    case 'q':
                        if(cart.Count()!=0)
                        {
                            int total=0;
                            foreach (var prod in cart)
                            {
                                int id= prod.Key;
                                int quantity= prod.Value;
                                Product? product= list.FirstOrDefault(p=>p?.Id== id);
                                if(product!=null)
                                {
                                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Quantity: {quantity}");
                                    total+= product.Price;

                                }
                            };

                            Console.WriteLine($"Subtotal: {total}\nTotal: {total*1.07}");
                        }

                        else Console.WriteLine("You have no items in cart. No total.");
                        break;

                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
    }


}
