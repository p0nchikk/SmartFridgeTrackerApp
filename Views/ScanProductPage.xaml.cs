using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class ScanProductPage : ContentPage
{
	public ScanProductPage()
	{
		InitializeComponent();
		BindingContext = new ScanProductViewModel();
	}

    //private void CameraView_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    //{

    //}
}