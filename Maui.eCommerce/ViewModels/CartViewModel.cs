using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class CartViewModel
    {
        public int Quantity
        {
            get
            {
                return Model?.Quantity ?? 0;
            }
            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public Product? Model { get; set; }

        public Product? AddOrUpdate()
        {
            var item= ShoppingCartService.Current.AddOrUpdate(Model);
            return item;
        }

        public CartViewModel() {
            Model = new Product();
        }

        public CartViewModel(Product? model)
        {
            Model = model;
        }
    }
}