using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class ProductInfoViewModel : ViewModelBase, IQueryAttributable
    {
        #region Variables Declaration

        private string? name = "No name";
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChange(); 
            }
        }

        private string? productImage = "image_icon.png";
        public string ProductImage
        {
            get { return productImage; }
            set { productImage = value;
                OnPropertyChange();
            }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value;
                OnPropertyChange();
            }
        }

        private double quantity;

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value;
                OnPropertyChange();
            }
        }


        #endregion

        #region Commands
        public ICommand? GoBackCommand {  get; set; }
        #endregion

        #region Constructor
        public ProductInfoViewModel()
        {
            GoBackCommand = new Command(GoBack);

        } 
        #endregion

        #region Functions
        private void GoBack()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//MainPage");
            });
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Product", out var productObj) &&
                productObj is Product product)
            {
                Name = product.Name;
                ProductImage = product.Image;
                Count = product.Count;
                Quantity = product.Quantity;
            }
        }
        #endregion
    }
}
