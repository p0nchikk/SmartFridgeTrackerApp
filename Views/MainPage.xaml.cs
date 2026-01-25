namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();	
		BindingContext = new MainViewModel();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((MainViewModel)BindingContext).LoadProductsAsync();
    }

}