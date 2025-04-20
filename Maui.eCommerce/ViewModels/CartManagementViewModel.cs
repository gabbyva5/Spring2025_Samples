using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class CartManagementViewModel : INotifyPropertyChanged
    {
        public Product? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ShoppingCartService cartService = ShoppingCartService.Current;
        private ProductServiceProxy inventoryProxy = ProductServiceProxy.Current;


        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Product?> Products
        {
            get
            {
                var filteredList = cartService.Cart.Items.Where(p => p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<Product?>(filteredList);
            }
        }

        public ObservableCollection<Product?> InventoryProducts
        {
            get
            {
                var filteredList = inventoryProxy.Products.Where(p => p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<Product?>(filteredList);
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));            
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshInventoryList()
        {
            NotifyPropertyChanged(nameof(InventoryProducts));
        }
        
        public void RefreshCartList()
        {
            NotifyPropertyChanged(nameof(Products));
        }
    }
}