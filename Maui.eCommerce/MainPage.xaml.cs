using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void CartClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//CartManagement");
        }
    }

}
