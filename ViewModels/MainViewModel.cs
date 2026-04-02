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
using SmartFridgeTracker.Views;

namespace SmartFridgeTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables Declaration
        private ObservableCollection<Product> expiringSoonProducts;
        public ObservableCollection<Product> ExpiringSoonProducts
        {
            get => expiringSoonProducts;
            set
            {
                expiringSoonProducts = value;
                OnPropertyChange();
            }
        }

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

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value;
                OnPropertyChange();
            }   
        }

        private int expiringSoonCount;
        public int ExpiringSoonCount
        {
            get { return expiringSoonCount; }
            set { expiringSoonCount = value;
                OnPropertyChange();
            }
        }

        private int expiredCount;
        public int ExpiredCount
        {
            get { return expiredCount; }
            set { expiredCount = value;
                OnPropertyChange();
            }
        }

        private string greeting;
        public string Greeting
        {       
            get { return greeting; }
            set { greeting = value;
                OnPropertyChange();
            }
        }
        #endregion

        #region Commands
        public ICommand? ProfileImageTappedCommand { get; set; }
        public ICommand? GoToAddProductPageCommand { get; set; }
        public ICommand? DecrementCountOfItemCommand { get; set; }
        public ICommand? RemoveItemCommand { get; set; }
        public ICommand? OnProductTappedCommand { get; }
        public ICommand? GoToFridgeInventoryCommand { get; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            Products = new ObservableCollection<Product>();
            ExpiringSoonProducts = new ObservableCollection<Product>();

            ProfileImageTappedCommand = new Command(ProfileImageTapped);
            GoToAddProductPageCommand = new Command(GoToAddProductPage);
            DecrementCountOfItemCommand = new Command(DecrementCountOfItem);
            OnProductTappedCommand = new Command(OnProductTapped);
            GoToFridgeInventoryCommand = new Command(GoToFridgeInventory);
        }

        #endregion

        #region Functions
        private void ProfileImageTapped()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("MyProfilePage");
            });
        }
        private void GoToAddProductPage()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("AddProductPage");
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
                await Shell.Current.GoToAsync("ProductInfoPage", navigationParameter);
            });
        }
        

        private void GoToFridgeInventory()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("FridgeInventoryPage");
            });

        }

        #region On Appearing
        //Load products from logged in user fridge
        public async Task LoadProductsAsync()
        {
            var instance = AppService.GetInstance();
            AuthUser user = instance.loggedInUser;

            if (user?.Fridge?.ProductsList != null)
            {
                Products.Clear();
                foreach (var p in user.Fridge.ProductsList)
                    Products.Add(p);

                ExpiringSoonProducts.Clear();
                List<Product> expSoonList = user.Fridge.GetExpiringSoonList();
                foreach (var p in expSoonList)
                    ExpiringSoonProducts.Add(p);

                TotalCount = Products.Count;
                ExpiringSoonCount = ExpiringSoonProducts.Count;
                ExpiredCount = user.Fridge.GetSpoiledCount();
            }
        }

        public async Task LoadGreetingAsync()
        {
            var instance = AppService.GetInstance();
            AuthUser user = instance.loggedInUser;
            if (user != null)
            {
                string timeOfDay = GetTimeOfDay();
                Greeting = $"Good {timeOfDay}, {user.FullName}!";
            }
        }

        private string GetTimeOfDay()
        {
            DateTime dt = DateTime.Now;
            if(dt.Hour < 12)
                return "Morning";
            else if (dt.Hour < 18)
                return "Afternoon";
            else
                return "Evening";
        }
        #endregion

        #endregion
    }
}
