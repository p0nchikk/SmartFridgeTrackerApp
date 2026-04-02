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
        private string? previewImage = "image_icon.png";
        public string? PreviewImage
        {
            get { return previewImage; }
            set
            {
                previewImage = value;
                OnPropertyChange();
            }
        }

        private FileResult? selectedImage;
        public FileResult? SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
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
        private async void SelectImage_Clicked()
        {
            SelectedImage = await MediaPicker.Default.PickPhotoAsync();
            IsCameraViewVisible = false;
            IsPreviewImageVisible = true;

            if (selectedImage != null)
            {
                //var stream = await selectedImage.OpenReadAsync();
                PreviewImage = selectedImage.FullPath;
            }
        }
        private async void TakePhoto_Clicked(CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
        {
            IsCameraViewVisible = false;
            IsPreviewImageVisible = true;
            PreviewImage = ImageSource.FromStream(() => e.Media).ToString(); //check if this works
            //send to api and get the product info
        }
        private async void UploadImage_Clicked()
        {
            //send to api and get the product info
        }
        #endregion
    }
}
