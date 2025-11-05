using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class ScanItemPage : ContentPage
{
	public ScanItemPage()
	{
		InitializeComponent();
		BindingContext = new ScanItemViewModel();
	}

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {

    }
}