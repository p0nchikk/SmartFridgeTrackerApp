using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables Declaration

        private ObservableCollection<ProductViewModel> products = new();
        public ObservableCollection<ProductViewModel> Products
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
        #endregion

        #region Constructor

        //public MainViewModel()
        //{
        //    // Initialize with demo user and products
        //    Products = new ObservableCollection<Product>
        //    {
        //        new Product { Name = "Milk Tnyva 3%", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/tnuva3_milk_1200x1200.png?v=1637090060", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false },
        //        new Product { Name = "Danone Bio Yogurt 3%", Quantity = 5, Image = "https://shoppy.co.il/cdn/shop/products/danonebioyogurt3_1200x1200.png?v=1636740481", ExpirationDate = DateTime.Now.AddDays(5), IsSpoiled = false },
        //        new Product { Name = "Emek Sliced cheese 28%", Quantity = 1, Image = "https://static.yango.tech/avatars/get-grocery-goods/2756334/2f2a22d4-60d8-445b-b983-5c98a1536299/300x300?webp=true", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false },
        //        new Product { Name = "Sliced Tea Salami - Tirat Tzvi", Quantity = 1, Image = "https://shoppy.co.il/cdn/shop/products/teasalami_580x.png?v=1641837594", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false },
        //        new Product { Name = "Organic Free-Range Eggs L", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/organiceggs_a76c1206-e41c-4362-9fcc-d6faa72ca115_580x.jpg?v=1637113809", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false },
        //        new Product { Name = "Yoplait Yogurt 0% - Strawberry", Quantity = 4, Image = "https://shoppy.co.il/cdn/shop/products/yoplaitstrawberry_1200x1200.png?v=1637084683", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false },
        //        };
        //}
        public MainViewModel()
        {
            // Raw demo products
        //    var demoProducts = new List<Product>
        //{
        //    new Product { Name = "Milk Tnyva 3%", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/tnuva3_milk_1200x1200.png?v=1637090060", ExpirationDate = DateTime.Now, IsSpoiled = true },
        //    new Product { Name = "Danone Bio Yogurt 3%", Quantity = 5, Image = "https://shoppy.co.il/cdn/shop/products/danonebioyogurt3_1200x1200.png?v=1636740481", ExpirationDate = DateTime.Now.AddDays(5), IsSpoiled = false },
        //    new Product { Name = "Emek Sliced cheese 28%", Quantity = 1, Image = "https://static.yango.tech/avatars/get-grocery-goods/2756334/2f2a22d4-60d8-445b-b983-5c98a1536299/300x300?webp=true", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false },
        //    new Product { Name = "Sliced Tea Salami - Tirat Tzvi", Quantity = 1, Image = "https://shoppy.co.il/cdn/shop/products/teasalami_580x.png?v=1641837594", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false },
        //    new Product { Name = "Organic Free-Range Eggs L", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/organiceggs_a76c1206-e41c-4362-9fcc-d6faa72ca115_580x.jpg?v=1637113809", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false },
        //    new Product { Name = "Yoplait Yogurt 0% - Strawberry", Quantity = 4, Image = "https://shoppy.co.il/cdn/shop/products/yoplaitstrawberry_1200x1200.png?v=1637084683", ExpirationDate = DateTime.Now, IsSpoiled = true }, 
        //};

        //    // Wrap each Product in ProductViewModel
        //    Products = new ObservableCollection<ProductViewModel>(
        //        demoProducts.Select(p => new ProductViewModel(p))
        //    );
            Products = new ObservableCollection<ProductViewModel>();

            ProfileImageTappedCommand = new Command(ProfileImageTapped);
            GoToScanItemPageCommand = new Command(GoToScanItemPage);
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

        #endregion
    }
}
