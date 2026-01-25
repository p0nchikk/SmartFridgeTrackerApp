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
        public string BarCode { set; get; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public double Amount { get; set; }
        public int Count { get; set; }
        public string? Fabricator { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string StateIcon
        {
            get
            {
                if (ExpirationDate.CompareTo(DateTime.Now) < 0) return "exclamation.png";
                // "Soon to expire" if 2 days or less left
                else if ((ExpirationDate - DateTime.Now).TotalDays <= 3) return "warning_sign.png";
                else return string.Empty;
            }
        }       

        public Product(string name, double quantity, string fabricator, string packaging)
        {
            this.Image = "image_icon.png";
            this.Name = name;
            this.Amount = quantity;
            this.Fabricator = fabricator;
            this.Count = 1;
            this.ExpirationDate = DateTime.FromOADate(7); //default expiration date 7 days from now; change later
            this.BarCode = string.Empty;
        }
    }
}
