using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Category
    {
        public string Name { get; set; } //Category Name
        public List<String> QuantityUnits { get; set; } = new List<String>(); //Quantity Units
    }
}
