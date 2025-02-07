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
            Cart= new Dictionary<int, int>();
        }
        public Dictionary<int, int> Cart { get; private set; }

        private List<Product?> Stock= ProductServiceProxy.Current.Products;

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
            if(product.Id==0)
                return product;


            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Product? product = Stock.FirstOrDefault(p => p?.Id == id);
            Stock.Remove(product);

            return product;
        }
    }
}
