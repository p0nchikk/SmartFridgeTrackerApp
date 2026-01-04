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
        private AuthUser? user;
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

        //Temprorary fake data generator
        private void CreateFakeData()
        {
            user = new AuthUser()
            {
                UserName = "Polina",
                Password = "1234",
                Email = "polina@gmail.com",
                RegDate = DateTime.Now,
            };
            fridgeName = "Amkor 2XL Delux";

            products = new();
            //Wrap each temprorary product to fridge
            products.Add(new Product { Name = "Milk Tnyva 3%", Count = 2, Image = "https://shoppy.co.il/cdn/shop/products/tnuva3_milk_1200x1200.png?v=1637090060", ExpirationDate = DateTime.Now, IsSpoiled = true });
            products.Add(new Product { Name = "Danone Bio Yogurt 3%", Count = 5, Image = "https://shoppy.co.il/cdn/shop/products/danonebioyogurt3_1200x1200.png?v=1636740481", ExpirationDate = DateTime.Now.AddDays(5), IsSpoiled = false });
            products.Add(new Product { Name = "Emek Sliced cheese 28%", Count = 1, Image = "https://static.yango.tech/avatars/get-grocery-goods/2756334/2f2a22d4-60d8-445b-b983-5c98a1536299/300x300?webp=true", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false });
            products.Add(new Product { Name = "Sliced Tea Salami - Tirat Tzvi", Count = 1, Image = "https://shoppy.co.il/cdn/shop/products/teasalami_580x.png?v=1641837594", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false });
            products.Add(new Product { Name = "Organic Free-Range Eggs L", Count = 2, Image = "https://shoppy.co.il/cdn/shop/products/organiceggs_a76c1206-e41c-4362-9fcc-d6faa72ca115_580x.jpg?v=1637113809", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false });
            products.Add(new Product { Name = "Yoplait Yogurt 0% - Strawberry", Count = 4, Image = "https://shoppy.co.il/cdn/shop/products/yoplaitstrawberry_1200x1200.png?v=1637084683", ExpirationDate = DateTime.Now, IsSpoiled = true });       
        }
        #region Getters and Setters
        //GET FRIDGE NAME
        public async Task<string> GetFridgeNameAsync()
        {
            return fridgeName;
        }
        //GET USER
        public async Task<AuthUser?> GetUserAsync()
        {
            return this.user;
        }
        //ADD USER
        public async Task<bool> AddUserAsync(AuthUser user)
        {
            this.user = user;
            return true;
        }
        //GET PRODUCTS
        public async Task<List<Product>> GetProductsAsync()
        {
            return products;
        }
        //GET PRODUCTS COUNT
        public async Task<int> GetProductCountAsync()
        {
            return products.Count;
        }
        //GET EXPIRING SOON PRODUCTS COUNT
        public async Task<int> GetExpinigSoonCountAsync()
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
        //GET SPOLIED PRODUCTS COUNT
        public async Task<int> GetSpoiledCountAsync()
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
        #endregion

        #region Functions
        //DECREMENT COUNT OF ITEM
        public async Task<bool> DecrementCountOfItemAsync(Product product)
        {
            if (products.Contains(product))
            {
                if (product.Count == 1)
                {
                    products.Remove(product);
                }
                else
                {
                    product.Count--;
                }
                return true;
            }
            return false;
        }
        //ADD PRODUCT 
        public async Task<bool> AddProductAsync(Product newProduct)
        {
            products.Add(newProduct);
            return true;
        }
        //REMOVE PRODUCT
        public async Task<bool> RemoveProductAsync(Product product)
        {
            products.Remove(product);
            return true;
        }
        #endregion
    }
}
