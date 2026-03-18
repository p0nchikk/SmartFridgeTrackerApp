using SmartFridgeTracker.ViewModels;

namespace SmartFridgeTracker.Views;

public partial class ScanProductPage : ContentPage
{
	public ScanProductPage()
	{
		InitializeComponent();
		BindingContext = new ScanProductViewModel();
	}

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {

    }
}