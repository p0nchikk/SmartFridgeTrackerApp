using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFridgeTracker.Models;

namespace SmartFridgeTracker.Services
{
    public class LocalDataService
    {
        private static LocalDataService? instance;
        private User? user;
        private List<Product> products;
        private string fridgeName = "";

        public LocalDataService()
        {
            CreateFakeData();
        }

        public static LocalDataService? GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalDataService();
            }
            return instance;
        }

        private void CreateFakeData()
        {
            user = new User()
            {
                UserName = "Polina",
                Password = "1234",
                Email = "polina@gmail.com",
                RegDate = DateTime.Now,
            };
            fridgeName = "Amkor 2XL Delux";

            products = new();
            //Wrap each temprorary product to fridge
            products.Add(new Product { Name = "Milk Tnyva 3%", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/tnuva3_milk_1200x1200.png?v=1637090060", ExpirationDate = DateTime.Now, IsSpoiled = true });
            products.Add(new Product { Name = "Danone Bio Yogurt 3%", Quantity = 5, Image = "https://shoppy.co.il/cdn/shop/products/danonebioyogurt3_1200x1200.png?v=1636740481", ExpirationDate = DateTime.Now.AddDays(5), IsSpoiled = false });
            products.Add(new Product { Name = "Emek Sliced cheese 28%", Quantity = 1, Image = "https://static.yango.tech/avatars/get-grocery-goods/2756334/2f2a22d4-60d8-445b-b983-5c98a1536299/300x300?webp=true", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false });
            products.Add(new Product { Name = "Sliced Tea Salami - Tirat Tzvi", Quantity = 1, Image = "https://shoppy.co.il/cdn/shop/products/teasalami_580x.png?v=1641837594", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false });
            products.Add(new Product { Name = "Organic Free-Range Eggs L", Quantity = 2, Image = "https://shoppy.co.il/cdn/shop/products/organiceggs_a76c1206-e41c-4362-9fcc-d6faa72ca115_580x.jpg?v=1637113809", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false });
            products.Add(new Product { Name = "Yoplait Yogurt 0% - Strawberry", Quantity = 4, Image = "https://shoppy.co.il/cdn/shop/products/yoplaitstrawberry_1200x1200.png?v=1637084683", ExpirationDate = DateTime.Now, IsSpoiled = true });       
        }

        public int GetProductCount()
        {
            return products.Count;
        }
        public bool AddProduct(Product newProduct)
        {
            products.Add(newProduct);
            return true;
        }
        public bool RemoveProduct(Product product)
        {
            products.Remove(product);
            return true;
        }

        public bool DecrementCountOfItem(Product product)
        {
            if (products.Contains(product))
            {
                if(product.Quantity == 1)
                {
                    products.Remove(product);
                }
                else
                {
                    product.Quantity--;
                }
                return true;
            }
            return false;
        }

        public string GetFridgeName()
        {
            return fridgeName;
        }

        public bool AddUser(User user)
        {
            this.user = user;
            return true;
        }
        public User? GetUser()
        {
            return this.user;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public int GetExpinigSoonCount()
        {
            int count = 0;
            for ( int i = 0; i < products.Count; i++ )
            {
                if ((products[i].ExpirationDate - DateTime.Now).TotalDays < 3 && !products[i].IsSpoiled)
                {
                    count++;
                }
            }
            return count;
        }
        
        public int GetSpoiledCount()
        {
            int count = 0;
            for ( int i = 0; i < products.Count; i++ )
            {
                if (products[i].IsSpoiled)
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsUserRegistered(string userName, string password)
        {
            if (user != null)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
