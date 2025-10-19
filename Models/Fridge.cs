using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Fridge
    {
        public string? Name { get; set; }
        public List<Product>? ProductsList { get; set; }
        public int ProductsCount { get; set; }
    }
}
