namespace SmartFridgeTracker.Views;

public partial class AddProductPage : ContentPage
{
	public AddProductPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.AddProductViewModel();
    }
}