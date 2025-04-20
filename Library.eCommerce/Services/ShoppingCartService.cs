using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ShoppingCartService()
        {
            Cart= new ShoppingCart();  
        }

        public ShoppingCart Cart { get; private set; }

        private static ShoppingCartService? instance;
        private static object instanceLock = new object();
        public static ShoppingCartService Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }

                return instance;
            }
        }

        public Product AddOrUpdate(Product product)
        {
            var inventoryProd= ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);

            if(inventoryProd.Quantity < product.Quantity)
                product.Quantity= inventoryProd.Quantity;    //if user wants more than is available, just give them the rest


            int diff;
            var prod = Cart.Items.FirstOrDefault(p => p.Id == product.Id);
            if (prod != null) 
            {
                if(product.Quantity < prod.Quantity)    
                {
                    prod.Quantity+= product.Quantity;
                    inventoryProd.Quantity-=product.Quantity;
                }
                else    //if the # in cart is less than new quantity
                {
                    prod.Quantity += product.Quantity;
                    inventoryProd.Quantity-=product.Quantity;
                }
            }
            else
            {
                Cart.Items.Add(product);
                inventoryProd.Quantity-=product.Quantity;
            }

            return prod;
        }


        public Product? Delete(Product product)
        {
            var prod = Cart.Items.FirstOrDefault(p => p.Id == product.Id);
            if (prod != null)
            {
                Cart.Items.Remove(prod);
            }

            return prod;
        }
    }
}
