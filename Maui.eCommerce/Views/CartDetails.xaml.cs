using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class CartDetails : ContentPage
{
    public CartDetails()
	{
		InitializeComponent();
	}

    public int ProductId { get; set; }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.AddOrUpdate();
        Shell.Current.GoToAsync("//CartManagement");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartManagement");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(ShoppingCartService.Current.GetById(ProductId) == null)
        {
            BindingContext = new CartViewModel();
        }
        else
        {
            BindingContext = new CartViewModel(ShoppingCartService.Current.GetById(ProductId)); 
        }
    }
}