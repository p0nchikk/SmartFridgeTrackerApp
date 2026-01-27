using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        #region Variables Declaration
        //Image
        private string? image = "image_icon.png";
        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChange(nameof(Image));
            }
        }
        //Message
        private string? message;
        public string? Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange(nameof(Message));
            }
        }
        //Product Name
        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChange(nameof(Name));
            }
        }        
        //Quantity
        private double quantity;
        public double Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChange(nameof(Quantity));
            }
        }
        //Count
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChange(nameof(Count));
            }
        }

        //Category
        //public static List<string> Categories = new List<string>{ "Vegetables", "Fruits", "Dairy", "Meat", "Beverages", "Snacks", "Grains", "Frozen Foods", "Condiments", "Bakery" };

        //Fabricator
        private string? fabricator;
        public string? Fabricator
        {
            get { return fabricator; }
            set
            {
                fabricator = value;
                OnPropertyChange(nameof(Fabricator));
            }
        }

        #endregion

        #region Commands
        public ICommand? GoBackCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand DecreaseCountCommand { get; set; }
        public ICommand IncreaseCountCommand { get; set; }
        #endregion

        #region Constructor
        public AddProductViewModel()
        {
            Count = 1;
            AddProductCommand = new Command(AddProduct);
            GoBackCommand = new Command(GoBack);
            DecreaseCountCommand = new Command(DecreaseCount);
            IncreaseCountCommand = new Command(IncreaseCount);
        }
        #endregion

        #region Functions
        private async void AddProduct()
        {
            //Reset message
            Message = string.Empty;
            //First, validate all fields are filled
            List<Object> entries = new List<Object> { Name, Quantity, Fabricator, Count };
            foreach (var entry in entries)
            {
                if (entry == null || (entry is string str && string.IsNullOrWhiteSpace(str)))
                {
                    Message = "Please fill in all fields.";
                    return;
                }
            }
            // Implementation for adding a product goes here
            Product newProduct = new Product(name, quantity, fabricator, count);

            var instance = AppService.GetInstance();
            bool result = await instance.LoadProduct(newProduct);
            if (result)
            {
                Message = "Your product is added succesfully!";
            }
            else
            {
                Message = "Something went wrong";
            }
        }
        private void GoBack() //Navigate to the previous page ( not neccessarily ScanPAge )
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//ScanItemPage");
            });
        }

        private void DecreaseCount()
        {
            if (Count > 1)
            {
                Count--;
            }
        }
        private void IncreaseCount()
        {
            Count++;
        }
        #endregion
    }
}
