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

        public string? Name => product.Name;
        public int Quantity => product.Quantity;
        public string? Image => product.Image;
        public DateTime ExpirationDate => product.ExpirationDate;
        public bool IsSpoiled => product.IsSpoiled;

        // UI-specific property
        public string StateIcon
        {
            get
            {
                if (IsSpoiled) return "warning_sign.png";
                // "Soon to expire" if 2 days or less left
                else if ((ExpirationDate - DateTime.Now).TotalDays <= 3) return "exclamation.png";
                else return string.Empty;
            }
        }
    }
}
