namespace SmartFridgeTracker.Views;
using SmartFridgeTracker.ViewModels;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
    private async void ButtonGoToRegister_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}