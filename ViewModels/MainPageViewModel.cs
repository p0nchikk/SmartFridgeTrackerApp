using SmartFridgeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Variables Declaration

        private User? currentUser;
        public User? CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChange(nameof(CurrentUser));
                // When user changes, update Products list
                Products = currentUser?.Fridge?.ProductsList ?? new ObservableCollection<Product>();
            }
        }

        private ObservableCollection<Product> products = new();
        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChange(nameof(Products));
            }
        }

        #endregion

        #region Commands

        public ICommand AddProductCommand { get; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            // Initialize with demo user and products
            CurrentUser = new User
            {
                UserName = "JohnDoe",
                RegDate = DateTime.Now,
                Fridge = new Fridge
                {
                    Name = "John's Fridge",
                    ProductsList = new ObservableCollection<Product>
                    {
                        new Product { Name = "Milk", Quantity = 2, Image = "milk.png", ExpirationDate = DateTime.Now.AddDays(3), IsSpoiled = false },
                        new Product { Name = "Apples", Quantity = 10, Image = "apple.png", ExpirationDate = DateTime.Now.AddDays(5), IsSpoiled = false },
                        new Product { Name = "Bananas", Quantity = 6, Image = "banana.png", ExpirationDate = DateTime.Now.AddDays(2), IsSpoiled = false }
                    }
                }
            };

            AddProductCommand = new Command(AddProduct);
        }

        #endregion

        #region Functions

        private void AddProduct()
        {
            // Create a new product (you can later extend this to take input)
            var newProduct = new Product
            {
                Name = "New Item",
                Quantity = 1,
                Image = "placeholder.png",
                ExpirationDate = DateTime.Now.AddDays(7),
                IsSpoiled = false
            };

            if (CurrentUser?.Fridge != null)
            {
                CurrentUser.Fridge.AddProduct(newProduct);
                // Raise property changed to notify UI
                OnPropertyChange(nameof(Products));
            }
        }

        #endregion
    }
}
