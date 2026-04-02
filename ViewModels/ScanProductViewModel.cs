using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;
using ZXing.QrCode.Internal;

namespace SmartFridgeTracker.ViewModels
{
    public class ScanProductViewModel : ViewModelBase
    {
        //change everything to photo and not barcode
        #region Variables Definition
        private byte[]? _currentImageBytes; //Hold the current image bytes for API upload

        private ImageSource previewImage = "image_icon.png";
        public ImageSource PreviewImage
        {
            get { return previewImage; }
            set
            {
                previewImage = value;
                OnPropertyChange();
            }
        }

        private bool isCameraViewVisible = true;
        public bool IsCameraViewVisible
        {
            get { return isCameraViewVisible; }
            set
            {
                isCameraViewVisible = value;
                OnPropertyChange();
            }
        }

        private bool isPreviewImageVisible = false;
        public bool IsPreviewImageVisible
        {
            get { return isPreviewImageVisible; }
            set
            {
                isPreviewImageVisible = value;
                OnPropertyChange();
            }
        }

        private string? message;
        public string? Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange();
            }
        }
        #endregion

        #region Commands 
        public ICommand? SelectImageCommand { get; set; }
        public ICommand? UploadImageCommand { get; set; }
        public ICommand? TakePhotoCommand { get; set; }
        #endregion

        #region Constructor
        public ScanProductViewModel()
        {
            InitializateAsyncCommands();
        }
        private async void InitializateAsyncCommands()
        {
            var cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
            SelectImageCommand = new Command(SelectImage_Clicked);
            TakePhotoCommand = new Command<CommunityToolkit.Maui.Views.MediaCapturedEventArgs>(TakePhoto_Clicked);
            UploadImageCommand = new Command(UploadImage_Clicked);
        }
        #endregion

        #region Functions
        public async void TakePhoto_Clicked(CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
        {
            if (e?.Media == null) return;
            //Capture the photo and convert it to byte array
            using var stream = e.Media;
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            _currentImageBytes = memoryStream.ToArray();

            ShowPreview(); //Show the preview after taking the photo
        }
        private async void SelectImage_Clicked()
        {         
            var result = await MediaPicker.Default.PickPhotoAsync();
            if (result != null)
            {
                //Read the selected photo and convert it to byte array
                using var stream = await result.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                // Save to our Single Source of Truth
                _currentImageBytes = memoryStream.ToArray();

                ShowPreview(); //Show the preview after selecting the photo
            }
        }

        /// <summary>
        /// Show the preview of the captured or selected photo. 
        /// This will update the UI to show the image and hide the camera view.
        /// </summary>
        private void ShowPreview()
        {
            if (_currentImageBytes != null)
            {
                PreviewImage = ImageSource.FromStream(() => new MemoryStream(_currentImageBytes));
                IsCameraViewVisible = false;
                IsPreviewImageVisible = true;
                Message = "Photo ready! Send it or retake.";
            }
        }
       
        private async void UploadImage_Clicked()
        {
            Message = "Sent successfully! Fetching product info...";
            //send to api and get the product info
        }
        #endregion
    }
}
