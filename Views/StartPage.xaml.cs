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
        await Task.Delay(2000);
        await Shell.Current.GoToAsync("//MyProfilePage");
    }
}