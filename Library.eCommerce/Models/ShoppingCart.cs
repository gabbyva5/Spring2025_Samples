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
            Items = new List<Product>();
        }
    }
}