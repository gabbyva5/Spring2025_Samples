using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CartManagementView : ContentPage
{
    public CartManagementView()
	{
		InitializeComponent();
		BindingContext = new CartManagementViewModel();
	}

    private void AddOrUpdateClicked(object sender, EventArgs e)
    {
        var productId = (BindingContext as CartManagementViewModel)?.SelectedProduct?.Id;
       // Shell.Current.GoToAsync($"//Cart?productId={productId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        //(BindingContext as CartManagementViewModel)?.Delete();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

	private void InventorySearchClicked(object sender, EventArgs e)
    {
        (BindingContext as CartManagementViewModel)?.RefreshInventoryList();
    }

    private void CartSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as CartManagementViewModel)?.RefreshCartList();
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartManagementViewModel)?.RefreshInventoryList();
        (BindingContext as CartManagementViewModel)?.RefreshCartList();
    }

}
