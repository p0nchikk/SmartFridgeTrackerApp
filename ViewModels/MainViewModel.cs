using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartFridgeTracker.Services;

namespace SmartFridgeTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables Declaration

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Commands
        public ICommand? ProfileImageTappedCommand { get; set; }
        public ICommand? GoToScanItemPageCommand { get; set; }
        public ICommand? DecrementCountOfItemCommand { get; set; }
        public ICommand? ProductTappedCommand { get; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            if (LocalDataService.GetInstance() != null)
            {
                //Calling async function for retreiving data from service
                InitializeAsyncFunctions();
            }
            else
            {
                Products = new ObservableCollection<Product>();
            }

            ProfileImageTappedCommand = new Command(ProfileImageTapped);
            GoToScanItemPageCommand = new Command(GoToScanItemPage);
            DecrementCountOfItemCommand = new Command(DecrementCountOfItem);
            ProductTappedCommand = new Command(OnProductTapped);
        }

        public async void InitializeAsyncFunctions()
        {
            Products = new ObservableCollection<Product>(await LocalDataService.GetInstance().GetProductsAsync());
        }

        #endregion

        #region Functions
        private void ProfileImageTapped()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//MyProfilePage");
            });
        }
        private void GoToScanItemPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//ScanItemPage");
            });
        }
        private async void DecrementCountOfItem(object objProduct)
        {
            Product product = objProduct as Product;
            if (product != null) 
            {
                if (await LocalDataService.GetInstance().DecrementCountOfItem(product))
                {
                    Products = new ObservableCollection<Product>(await LocalDataService.GetInstance().GetProductsAsync()); //here I don't need await because I alr have products
                }
                else
                {
                    // TODO: to throw error
                }
            }
        }
        private void OnProductTapped(object product)
        {
            if (product == null)
                return;

            // navigate and pass the product as a parameter
            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product }
            };

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//ProductInfoPage", navigationParameter);
            });
        }

        #endregion
    }
}
