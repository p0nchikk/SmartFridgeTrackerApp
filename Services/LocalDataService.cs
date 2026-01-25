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
                FullName = "Polina",
                Password = "1234",
                Email = "polina@gmail.com",
                RegDate = DateTime.Now,
            };
            fridgeName = "Amkor 2XL Delux";
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
