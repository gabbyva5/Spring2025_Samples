using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;

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
        var inventoryProduct = ProductServiceProxy.Current.GetById(ProductId); 
        var prodInCart = ShoppingCartService.Current.GetById(ProductId); 

        if(prodInCart == null)
        {
            if (inventoryProduct != null)
            {
                var newProd = new Product
                {
                    Id = inventoryProduct.Id,
                    Name = inventoryProduct.Name,
                    Price = inventoryProduct.Price,
                    Quantity = 1 
                };
                BindingContext = new CartViewModel(newProd);
            }
            else
            {
                Shell.Current.GoToAsync("//CartManagement"); 
            }
        }
        else 
        {
            var newProd = new Product
                {
                    Id = prodInCart.Id,
                    Name = prodInCart.Name,
                    Price = prodInCart.Price,
                    Quantity = prodInCart.Quantity
                };
            BindingContext = new CartViewModel(newProd);
        }
    }
}