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
        public int LifeDays => product.LifeDays;

        public Color StatusColor
        {
            get
            {
                if (LifeDays <= 2)
                    return (Color)Application.Current.Resources["RedBright"];
                else if (LifeDays <= 5)
                    return (Color)Application.Current.Resources["SecondaryBright"];
                else
                    return (Color)Application.Current.Resources["PrimaryBright"];
            }
        }
    }
}
