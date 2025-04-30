using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace Maui.eCommerce.ViewModels;

public class CheckoutViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Product> ReceiptItems 
    {
        get
        {
            var list = ShoppingCartService.Current.Cart.Items?.Where(item => item != null).ToList()?? new List<Product>();
            return new ObservableCollection<Product>(list);
        }  
    }
    public double Subtotal 
    {
        get
        {
            return ReceiptItems.Sum(item => (item?.Price ?? 0) * (item?.Quantity ?? 0));
        }
    }
    public double TaxAmount
    {
        get
        {
            return Subtotal * 0.07;
        }
    }
    public double Total 
    {
        get
        {
            return Subtotal + TaxAmount; 
        }
    }


    public void RefreshReceipt()
    {
        NotifyPropertyChanged(nameof(ReceiptItems));
        NotifyPropertyChanged(nameof(Subtotal));
        NotifyPropertyChanged(nameof(TaxAmount));
        NotifyPropertyChanged(nameof(Total));
    }

    public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (propertyName is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}