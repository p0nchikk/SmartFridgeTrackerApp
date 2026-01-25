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
        public List<Product> ProductsList { get; set; } = new List<Product>();

        public Fridge()
        {
            ProductsList = new List<Product>();
        }
        //Get total count of products expiring soon (within 3 days)
        public int GetExpiringSoonCount()
        {
            int count = 0;
            DateTime now = DateTime.Now;
            foreach (var product in ProductsList)
            {
                // Check if the product is expiring within the next 3 days
                if ((product.ExpirationDate - now).TotalDays <= 3 && product.ExpirationDate >= now)
                {
                    count += product.Count; // Consider the Count of each product
                }
            }
            return count;
        }
        //Get total count of spoiled products
        public int GetSpoiledCount()
        {
            int count = 0;
            DateTime now = DateTime.Now;
            foreach (var product in ProductsList)
            {
                // Check if the product is spoiled
                if (product.ExpirationDate < now)
                {
                    count += product.Count; // Consider the Count of each product
                }
            }
            return count;
        }
        //Add product to the fridge
        public void AddProduct(Product product)
        {
            if (product == null) return;

            // Find if a product with the same name already exists
            var existing = ProductsList.FirstOrDefault(p =>
                string.Equals(p.Name, product.Name, StringComparison.OrdinalIgnoreCase));

            //StringComparison.OrdinalIgnoreCase → tells it to compare without case sensitivity
            if (existing != null)
            {
                // Product already exists → increase its Count
                existing.Count += product.Count;
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
