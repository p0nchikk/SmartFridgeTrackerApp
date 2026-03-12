using SmartFridgeTracker.Services;

namespace SmartFridgeTracker.Views;

public partial class StartPage : ContentPage
{
	public StartPage()
    {
        InitializeComponent();
        StartSplashAsync();
    }
    //private async void StartSplashAsync()
    //{
    //    await Task.Delay(2000);
    //    await Shell.Current.GoToAsync("//LoginPage");
    //}
    private async void StartSplashAsync()
    {
        await Task.Delay(2000);
        await AppService.GetInstance().TryLogin("cherry@gmail.com", "123456");
            await Shell.Current.GoToAsync("//MainPage");
    }
}