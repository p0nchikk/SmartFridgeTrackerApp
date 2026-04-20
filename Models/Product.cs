using SmartFridgeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Product
    {
        public string Id { get; set; } 
        public string Emoji { get; set; }
        public string? Name { get; set; }
        public Quantity Quantity { get; set; }       
        public int LifeDays { get; set; } //in days
        public DateTime DateAdded { get; set; } = DateTime.Now; //to calculate expiration date and for sorting by date added

        #region Properties for future use
        //public Category Category { get; set; } = new Category();
        //public bool IsFrequent { get; set; } //for future use, to mark products that are added often by the user
        #endregion

        public Product(){ } //Parameterless constructor for Firebase deserialization
        public Product(string id, string emoji, string name, Quantity quantity, int count, int lifeDays)
        {
            this.Id = id;
            this.Emoji = emoji;
            this.Name = name;
            this.Quantity = quantity;
            this.LifeDays = lifeDays;
        }

        //Constructor without id, for new products that haven't been saved to Firebase yet
        public Product(string emoji, string name, float quantity, string measurement, int count, int lifeDays)
        {
            this.Id = string.Empty;
            this.Emoji = emoji;
            this.Name = name;
            this.Quantity = new Quantity(quantity, count, measurement);
            this.LifeDays = lifeDays;
        }
    }
}
