using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly Product product;

        public ProductViewModel(Product product)
        {
            this.product = product;
        }

        public string Name => product.Name;
        public string Emoji => product.Emoji;
        public string Quantity => product.Quantity;
        public int Count => product.Count;
        public int DaysLeft
        {
            get  
            {
                int daysLeft = product.LifeDays - (DateTime.Now - product.DateAdded).Days;
                return daysLeft >= 0 ? daysLeft : 0; // Return 0 if the product is already expired
            }
        }

        public Color StatusColor
        {
            get
            {
                if (DaysLeft <= 2)
                    return (Color)Application.Current.Resources["RedBright"];
                else if (DaysLeft <= 5)
                    return (Color)Application.Current.Resources["SecondaryBright"];
                else
                    return (Color)Application.Current.Resources["PrimaryBright"];
            }
        }
    }
}
