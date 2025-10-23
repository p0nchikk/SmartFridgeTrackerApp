using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.Models
{
    public class Fridge
    {
        public string? Name { get; set; }
        public ObservableCollection<Product> ProductsList { get; set; } = new ObservableCollection<Product>();
        public int ProductsCount => ProductsList.Count; // computed property

        public void AddProduct(Product product)
        {
            if (product == null) return;

            // Find if a product with the same name already exists
            var existing = ProductsList.FirstOrDefault(p =>
                string.Equals(p.Name, product.Name, StringComparison.OrdinalIgnoreCase));

            //StringComparison.OrdinalIgnoreCase → tells it to compare without case sensitivity

            if (existing != null)
            {
                // Product already exists → increase its quantity
                existing.Quantity += product.Quantity;
            }
            else
            {
                // Add as a new product
                ProductsList.Add(product);
            }
        }
        public bool RemoveProduct(Product product)
        {
            return ProductsList.Remove(product);
        }
    }
}
