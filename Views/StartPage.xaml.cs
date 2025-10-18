namespace SmartFridgeTracker.Views;

public partial class StartPage : ContentPage
{
	public StartPage()
    {
        InitializeComponent();
        StartSplashAsync();
    }
    private async void StartSplashAsync()
    {
        await Task.Delay(4000);
        //Application.Current.MainPage = new LoginPage();
        await Navigation.PushAsync(new LoginPage());
    }
}