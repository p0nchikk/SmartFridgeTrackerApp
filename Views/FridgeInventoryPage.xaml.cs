using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class FridgeInventoryPage : ContentPage
{
	public FridgeInventoryPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.FridgeInventoryViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((FridgeInventoryViewModel)BindingContext).LoadProductsAsync();
    }
}