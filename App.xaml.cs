using SmartFridgeTracker.Services;
using SmartFridgeTracker.Shells;
using SmartFridgeTracker.Views;

namespace SmartFridgeTracker
{
    public partial class App : Application
    {
        public App()
        {
            LocalDataService.GetInstance();
            InitializeComponent();
            MainPage = new AuthShell();
        }

        public void SetAppShell()
        {
            MainPage = new AppShell();
        }
        public void SetAuthShell()
        {
            MainPage = new AuthShell();
        }
    }
}
