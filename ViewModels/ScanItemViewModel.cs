using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing.QrCode.Internal;

namespace SmartFridgeTracker.ViewModels
{
    public class ScanItemViewModel
    {
        #region Variables Definition
        private QRCode? _QRCode;

        public QRCode? QRCode
        {
            get { return _QRCode; }
            set { _QRCode = value; }
        }
        private string message;

        public string Message
        {
            get { return message; } 
            set { message = value; }
        }
        #endregion

        #region Commands 
        public ICommand BarcodesDetectedCommand { get; set; }
        public ICommand GoToAddProductPageCommand { get; set; }
        #endregion

        #region Constructor
        public ScanItemViewModel()
        {
            BarcodesDetectedCommand = new Command(BarcodesDetected);
            GoToAddProductPageCommand = new Command(GoToAddProductPage);
        }
        #endregion

        #region Functions
        private void BarcodesDetected(/*object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e*/)
        {

        }
        private void GoToAddProductPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//AddProductPage");
            });
        }
        #endregion
    }
}
