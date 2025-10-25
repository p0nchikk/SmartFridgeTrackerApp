namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainViewModel();
	}
}