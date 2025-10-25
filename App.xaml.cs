using SmartFridgeTracker.Views;
namespace SmartFridgeTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current.UserAppTheme = AppTheme.Light;
            // MainPage = new AppShell();
            MainPage = new AppShell();
        }
    }
}
