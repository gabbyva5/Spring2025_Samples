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

        public Product? AddOrUpdate(Product product)
        {
            var inventoryProd= ProductServiceProxy.Current.GetById(product.Id);
            var prodInCart = GetById(product.Id);

            if(inventoryProd != null)
            {    
                int cartQuan= prodInCart?.Quantity ?? 0;
                if(inventoryProd.Quantity + cartQuan< product.Quantity)
                    product.Quantity= inventoryProd.Quantity + cartQuan;    //if user wants more than is available, just give them the rest


                if(product.Quantity==0)
                {
                    if(prodInCart != null)
                    {
                        inventoryProd.Quantity+= prodInCart.Quantity;   //if quan is 0, prod will be removed from cart but will remain in inventory
                        Cart.Items?.Remove(prodInCart);
                    }
                    return prodInCart;
                }
                else if(prodInCart != null) 
                {
                    int diff;

                    if(product.Quantity < prodInCart.Quantity)    
                    {
                        diff= prodInCart.Quantity- product.Quantity;
                        prodInCart.Quantity=product.Quantity;
                        inventoryProd.Quantity+=diff;
                    }
                    else    //if the # in cart is less than new quantity
                    {
                        diff= product.Quantity- prodInCart.Quantity;
                        prodInCart.Quantity= product.Quantity;
                        inventoryProd.Quantity-=diff;
                    }

                    return prodInCart;
                }
                else
                {
                    Cart.Items?.Add(product);
                    inventoryProd.Quantity-=product.Quantity;
                    return product;
                }
            }

            else return null;    //return null if attempted product does not exist
        }

        public Product? Delete(int id)
        {
            var cartProd = Cart.Items?.FirstOrDefault(p => p.Id == id);
            var inventoryProd= ProductServiceProxy.Current.Products.FirstOrDefault(p => p?.Id == id);
            if (cartProd != null && inventoryProd != null)
            {
                inventoryProd.Quantity+=cartProd.Quantity;  //add product quantity back to inventory
                Cart.Items?.Remove(cartProd);
            }

            return cartProd;
        }

        public Product? GetById(int id)
        {
            return Cart.Items?.FirstOrDefault(p => p.Id == id);
        }
    }
}