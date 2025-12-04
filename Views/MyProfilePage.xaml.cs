using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class MyProfilePage : ContentPage
{
	MyProfileViewModel vm;
    public MyProfilePage()
	{
		InitializeComponent();
		vm = new MyProfileViewModel();
		BindingContext = vm;
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		vm.UpdateFields();
	}
}