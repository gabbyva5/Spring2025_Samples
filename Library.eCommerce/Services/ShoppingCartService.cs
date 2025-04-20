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
            var inventoryProd= ProductServiceProxy.Current.GetById(product.Id);

            if(inventoryProd.Quantity < product.Quantity)
                product.Quantity= inventoryProd.Quantity;    //if user wants more than is available, just give them the rest


            int diff;
            var prodInCart = GetById(product.Id);
            if(product.Quantity==0)
            {
                if(prodInCart != null)
                {
                    inventoryProd.Quantity+= prodInCart.Quantity;   //if quan is 0, prod will be removed from cart but will remain in inventory
                    Cart.Items.Remove(prodInCart);
                }
            }
            else if(prodInCart != null) 
            {
                if(product.Quantity < prodInCart.Quantity)    
                {
                    prodInCart.Quantity=product.Quantity;
                    inventoryProd.Quantity+=product.Quantity;
                }
                else    //if the # in cart is less than new quantity
                {
                    prodInCart.Quantity += product.Quantity;
                    inventoryProd.Quantity-=product.Quantity;
                }
            }
            else
            {
                Cart.Items.Add(product);
                inventoryProd.Quantity-=product.Quantity;
            }

            return prodInCart;
        }

        public Product? Delete(Product product)
        {
            var cartProd = Cart.Items.FirstOrDefault(p => p.Id == product.Id);
            if (cartProd != null)
            {
                var inventoryProd= ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);
                inventoryProd.Quantity+=cartProd.Quantity;  //add product quantity back to inventory
                Cart.Items.Remove(cartProd);
            }

            return cartProd;
        }

        public Product? GetById(int id)
        {
            return Cart.Items.FirstOrDefault(p => p.Id == id);
        }
    }
}
