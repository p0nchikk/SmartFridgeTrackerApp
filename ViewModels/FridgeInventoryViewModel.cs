using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class FridgeInventoryViewModel : ViewModelBase
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
        public ICommand? OnProductTappedCommand { get; }
        public ICommand? DecrementCountOfItemCommand { get; set; }
        public ICommand? RemoveItemCommand { get; set; }

        #endregion

        #region Constructor
        public FridgeInventoryViewModel()
        {
            Products = new ObservableCollection<Product>();
            OnProductTappedCommand = new Command(OnProductTapped);
            DecrementCountOfItemCommand = new Command(DecrementCountOfItem);
        }
        #endregion

        #region Functions
        //Load products from logged in user fridge
        public async Task LoadProductsAsync()
        {
            var instance = AppService.GetInstance();
            AuthUser user = instance.loggedInUser;

            if (user?.Fridge?.ProductsList != null)
            {
                // Products.Clear();
                Products = new ObservableCollection<Product>();
                foreach (var p in user.Fridge.ProductsList)
                    Products.Add(p);
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
                await Shell.Current.GoToAsync("ProductInfoPage", navigationParameter);
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

        #endregion
    }
}
