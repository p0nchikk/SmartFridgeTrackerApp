using SmartFridgeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Product : ViewModelBase
    {
        public string Id { get; set; } 
        public string? Name { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public int Count { get; set; } //think about        
        public int LifeDays { get; set; } //in days

        #region Properties for future use
        //public string? Image { get; set; }
        //public Category Category { get; set; } = new Category();
        //public bool IsFrequent { get; set; } //for future use, to mark products that are added often by the user
        #endregion

        public Product(){ } //Parameterless constructor for Firebase deserialization
        public Product(string id, string name, double quantity, string quantityUnit, int count, int lifeDays)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.QuantityUnit = quantityUnit;
            this.Count = count;
            this.LifeDays = lifeDays;
        }

        //Constructor without id, for new products that haven't been saved to Firebase yet
        public Product(string name, double quantity, string quantityUnit, int count, int lifeDays)
        {
            this.Id = String.Empty;
            this.Name = name;
            this.Quantity = quantity;
            this.QuantityUnit = quantityUnit;
            this.Count = count;
            this.LifeDays = lifeDays;
        }
    }
}
