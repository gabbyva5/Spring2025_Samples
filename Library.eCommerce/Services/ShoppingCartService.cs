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
                    if(instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }

                return instance;
            }
        }

        public Product? AddOrUpdateCart(Product product)
        {
            var inventoryProd = ProductServiceProxy.Current.GetById(product.Id);
            var prodInCart = GetById(product.Id);

            if(inventoryProd == null) 
                return null;

            if(prodInCart == null)     //triggered by AddtoCart and item isn't alr in can
            {
                if(product.Quantity == 0)
                    return null;

                if(inventoryProd.Quantity < product.Quantity)
                    product.Quantity = inventoryProd.Quantity;

                Cart.Items?.Add(product);
                inventoryProd.Quantity-= product.Quantity;
                return product;
            }
            else    //triggered by AddtoCart when item is alr in cart or when quantity is updated from shopping list
            {
                if (product.Quantity == 0)
                {
                    inventoryProd.Quantity += prodInCart.Quantity;
                    Cart.Items?.Remove(prodInCart);     //remove from cart but still in inventory
                    return prodInCart;
                }

                int diff;
                if(product.Quantity>prodInCart.Quantity)    //more than in cart
                {
                    if(inventoryProd.Quantity + prodInCart.Quantity< product.Quantity)
                        product.Quantity= inventoryProd.Quantity + prodInCart.Quantity;
                    
                    diff= product.Quantity - prodInCart.Quantity;
                    prodInCart.Quantity += diff;
                    inventoryProd.Quantity -= diff;
                }
                else    //also catches if product.Quantity==prodInCart.Quantity
                {
                    diff= prodInCart.Quantity-product.Quantity;
                    prodInCart.Quantity -= diff; 
                    inventoryProd.Quantity += diff; 
                }

                return prodInCart;
            }
        }


        public Product? Delete(int id)
        {
            var cartProd= Cart.Items?.FirstOrDefault(p => p.Id == id);
            var inventoryProd= ProductServiceProxy.Current.Products.FirstOrDefault(p => p?.Id == id);
            if(cartProd != null && inventoryProd != null)
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