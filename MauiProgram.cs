using Microsoft.Extensions.Logging;
using SmartFridgeTracker.Services;
using ZXing.Net.Maui.Controls;


namespace SmartFridgeTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppService.GetInstance().Init();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("DMSans-Regular.ttf", "DMSansRegular");
                    fonts.AddFont("DMSans-ExtraBold.ttf", "DMSansExtraBold");
                    fonts.AddFont("AlanSans-Regular.ttf", "AlanSansRegular");
                    fonts.AddFont("AlanSans-ExtraBold.ttf", "AlanSansExtraBold");
                })
            .UseBarcodeReader();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
