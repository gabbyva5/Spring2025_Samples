using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class ShoppingCart
    {
        public List<Product>? Items { get; set; }

        public ShoppingCart() 
        { 
            Items = new List<Product>
            {
                new Product{Id = 1, Name ="Product 1", Quantity= 2, Price=12.99},
                new Product{Id = 3, Name ="Product 3", Quantity=1, Price=4.00}
            };
        }
    }
}