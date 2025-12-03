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

        public ICommand? RemoveItemCommand { get; set; }
        public ICommand? ProductTappedCommand { get; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            if (LocalDataService.GetInstance() != null)
            {
                Products = new ObservableCollection<Product>(LocalDataService.GetInstance().GetProducts());
            }
            else
            {
                Products = new ObservableCollection<Product>();
            }

            ProfileImageTappedCommand = new Command(ProfileImageTapped);
            GoToScanItemPageCommand = new Command(GoToScanItemPage);
            RemoveItemCommand = new Command(RemoveItem);
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
        private void RemoveItem(object objProduct)
        {
            Product product = objProduct as Product;
            if (product != null) 
            {
                //Products.Remove(product);
                if (LocalDataService.GetInstance().RemoveProduct(product))
                {
                    Products.Remove(product);
                } else
                {
                    // TODO: error
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
