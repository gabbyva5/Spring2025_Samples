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
            Cart= new Dictionary<int, int>();   //stores id and quantity
        }
        public Dictionary<int, int> Cart { get; private set; }

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

        public Product Add(Product product, int quan)
        {
            if(Cart.ContainsKey(product.Id))
                Update(product, Cart[product.Id]+quan);     //if item already exists in cart, update quantity
            else
            {
                if(quan<=product.Quantity)
                {
                    Cart[product.Id]= quan;
                    product.Quantity-=quan;
                }
            }

            return product;
        }

        public Product Update(Product product, int quan)
        {
            int quantity= Cart[product.Id];
            int diff= quan-quantity;    //desired quantity - current quantity

            if(diff<0)  //reduces quantity
            {
                Cart[product.Id]= quantity+diff;
                product.Quantity-=diff; 

                if(Cart[product.Id]==0)
                    Cart.Remove(product.Id);
            }
            else if(diff==product.Quantity)
            {
                Cart[product.Id]= quantity+diff;
                product.Quantity=0;
            }
            else if(diff>product.Quantity)  //nothing happens if there's not enough inventory in stock
                return product;
            else if(diff<product.Quantity)
            {
                Cart[product.Id]= quantity+diff;
                product.Quantity-= diff;
            }

            return product;
        }


        public Product? Delete(Product product)
        {
            if(Cart.ContainsKey(product.Id))    //nothing happens if not in cart
            {
                product.Quantity+= Cart[product.Id];
                Cart.Remove(product.Id);
            }
            
            return product;
        }
    }
}
