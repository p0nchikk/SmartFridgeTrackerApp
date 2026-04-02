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

        #region Think About
        //private string? productImage = "image_icon.png";
        //public string ProductImage
        //{
        //    get { return productImage; }
        //    set { productImage = value;
        //        OnPropertyChange();
        //    }
        //}
        #endregion

        //Emoji
        private string? emoji;
        public string? Emoji
        {
            get { return emoji; }
            set { emoji = value; 
                OnPropertyChange();
            }
        }


        //Name
        private string? name = "No name";
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChange(); 
            }
        }

        //Quantity ( quantity + unit)
        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChange();
            }
        }

        //Count
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value;
                OnPropertyChange();
            }
        }

        //Life Days ( good for )
        private string lifeDays;
        public string LifeDays
        {
            get { return lifeDays; }
            set { lifeDays = value;
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
                Emoji = product.Emoji;
                Name = product.Name;
                Quantity = product.Quantity;
                Count = product.Count;
                LifeDays = $"{product.LifeDays} days";
            }
        }
        #endregion
    }
}
