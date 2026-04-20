using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Quantity
    {
        public float CurrentAmount { get; set; }
        public float TotalAmount { get; set; }
        public string QuantityUnit { get; set; }

        public Quantity() { }

        public Quantity(float currentAmount, float totalAmount, string quantityUnit)
        {
            CurrentAmount = currentAmount;
            TotalAmount = totalAmount;
            QuantityUnit = quantityUnit;
        }

        public Quantity(float SingleAmount, int count, string quantityUnit)
        {
            float currentAmount = SingleAmount * count;
            CurrentAmount = currentAmount;
            TotalAmount = currentAmount;
            QuantityUnit = quantityUnit;
        }
    }
}
