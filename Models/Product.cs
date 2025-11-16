using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsSpoiled { get; set; }
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
