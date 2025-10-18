namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}