using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class MyProfilePage : ContentPage
{
	public MyProfilePage()
	{
		InitializeComponent();
		BindingContext = new MyProfileViewModel();
	}
}