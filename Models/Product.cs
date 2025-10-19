using System;
using System.Collections.Generic;
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
    }
}
