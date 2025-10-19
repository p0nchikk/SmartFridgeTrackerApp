using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
    private async void ButtonGoToLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}