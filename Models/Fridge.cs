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
            foreach (var product in ProductsList.Where(p => p.LifeDays <= 3))
            {
                 count++;
            }
            return count;
        }

        public List<Product> GetExpiringSoonList()
        {
            List<Product> list = new List<Product>();
            foreach (Product product in ProductsList.Where(p => p.LifeDays <= 3))
            {
                list.Add(product);
            }
            return list;
        }

        //Get total count of spoiled products
        public int GetSpoiledCount()
        {
            int count = 0;
            DateTime now = DateTime.Now;
            foreach (var product in ProductsList)
            {
                // Check if the product is spoiled
                if (product.LifeDays < 0)
                {
                    count++; // Consider the Count of each product
                }
            }
            return count;
        }

        //Add product to the fridge
        public void AddProduct(Product product)
        {
            if (product == null) return;
                ProductsList.Add(product);
        }
        public bool RemoveProduct(Product product)
        {
            return ProductsList.Remove(product);
        }
    }
}
