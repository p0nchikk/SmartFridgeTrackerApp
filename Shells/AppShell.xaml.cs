using SmartFridgeTracker.Views;

namespace SmartFridgeTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MyProfilePage), typeof(MyProfilePage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(ScanProductPage), typeof(ScanProductPage));
            Routing.RegisterRoute(nameof(ProductInfoPage), typeof(ProductInfoPage));
            Routing.RegisterRoute(nameof(FridgeInventoryPage), typeof(FridgeInventoryPage));
        }
    }
}
