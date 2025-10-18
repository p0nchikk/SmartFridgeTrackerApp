namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class RegisterPage : ContentView
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}