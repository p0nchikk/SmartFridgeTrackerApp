using SmartFridgeTracker.ViewModels;
using System.Linq.Expressions;

namespace SmartFridgeTracker.Views;

public partial class ScanProductPage : ContentPage
{
    public ScanProductPage()
    {
        InitializeComponent();
        BindingContext = new ScanProductViewModel();
    }
    private async void OnTakePhoto_Clicked(object sender, EventArgs e)
    {
        await myCamera.CaptureImage(CancellationToken.None);
    }
    private void OnMediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        ScanProductViewModel viewModel = (ScanProductViewModel)BindingContext;
        viewModel.TakePhoto_Clicked(e);
    }
        
}