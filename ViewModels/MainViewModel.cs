using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartFridgeTracker.Services;
using System.Reactive.Linq;

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
        public ICommand? RemoveItemCommand { get; set; }
        public ICommand? ProductTappedCommand { get; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            Products = new ObservableCollection<Product>();

            ProfileImageTappedCommand = new Command(ProfileImageTapped);
            GoToScanItemPageCommand = new Command(GoToScanItemPage);
            DecrementCountOfItemCommand = new Command(DecrementCountOfItem);
            ProductTappedCommand = new Command(OnProductTapped);
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
                if (await AppService.GetInstance().DecrementCountOfItemAsync(product))
                {
                    await LoadProductsAsync();
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
        //Load products from logged in user fridge
        public async Task LoadProductsAsync()
        {
            var instance = AppService.GetInstance();
            AuthUser user = instance.fullDetailsLoggedInUser;

            if (user?.Fridge?.ProductsList != null)
            {
                Products.Clear();
                foreach (var product in user.Fridge.ProductsList)
                    Products.Add(product);
            }
        }

        #endregion
    }
}
