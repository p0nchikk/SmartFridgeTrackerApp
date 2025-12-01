using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class ProductInfoPage : ContentPage
{
	public ProductInfoPage()
	{
		InitializeComponent();
        BindingContext = new ProductInfoViewModel();
    }
}