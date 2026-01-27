using SmartFridgeTracker.Services;
using SmartFridgeTracker.Views;

namespace SmartFridgeTracker
{
    public partial class App : Application
    {
        public App()
        {
            LocalDataService.GetInstance();
            InitializeComponent();
            MainPage = new AppShell()
            {
                FlowDirection = FlowDirection.LeftToRight,
            };
        }
        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new NavigationPage(new MainPage()));
        //}
    }
}
