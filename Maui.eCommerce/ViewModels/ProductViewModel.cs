﻿using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name 
        { 
            get
            {
                return Model?.Name ?? string.Empty;
            }

            set
            {
                if(Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public int Quantity
        {
            get
            {
                return Model?.Quantity ?? 0;
            }
            set
            {
                if(Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }
 
        public double Price
        {
            get
            {
                return Model?.Price ?? 0;
            }
            set
            {
                if(Model != null && Model.Price != value)
                {
                    Model.Price = value;
                 }
            }
        }


        public Product? Model { get; set; }

        public Product? AddOrUpdate()
        {
            return ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public ProductViewModel() {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;
        }
    }
}
