using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CheckoutDetails : ContentPage
{
    public CheckoutDetails()
    {
        InitializeComponent();       
        BindingContext= new CheckoutViewModel();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel)?.RefreshReceipt();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartManagement");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext= new CheckoutViewModel();
        (BindingContext as CheckoutViewModel)?.RefreshReceipt();
    }
}