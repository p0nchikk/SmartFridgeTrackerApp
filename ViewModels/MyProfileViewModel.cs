using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class MyProfileViewModel : ViewModelBase
    {
        #region Variables Declaration

        // Identity

        //private int achivements;
        //public int Achivements
        //{
        //    get { return achivements; }
        //}

        private string? username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChange(nameof(Username));
            }
        }

        private string? email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value; 
                OnPropertyChange(nameof(Email));
            }
        }

        private DateTime regDate;
        public DateTime RegDate
        {
            get { return regDate; }
            set
            {
                regDate = value;
                OnPropertyChange(nameof(RegDate));
            }
        }


        // Fridge Summary
        private string? fridgeName;
        public string FridgeName
        {
            get { return fridgeName; }
            set
            {
                fridgeName = value;
                OnPropertyChange(nameof(FridgeName));
            }
        }

        private int productCount;
        public int ProductCount
        {
            get
            {  return productCount; }
            set
            {
                productCount = value;
                OnPropertyChange(nameof(ProductCount));
            }
        }

        private int expiringSoonCount;
        public int ExpiringSoonCount
        {
            get { return expiringSoonCount; }
            set
            {
                expiringSoonCount = value;
                OnPropertyChange(nameof(ExpiringSoonCount));
            }
        }

        private int spoiledCount;
        public int SpoiledCount
        {
            get { return spoiledCount; }
            set
            {
                spoiledCount = value;
                OnPropertyChange(nameof(SpoiledCount));
            }
        }

        // Activity/Insights
        //private DateTime lastUpdated;
        //public DateTime LastUpdated
        //{
        //    get { return lastUpdated; }
        //}

        //private int weeklyUsage;
        //public int WeeklyUsage
        //{
        //    get { return weeklyUsage; }
        //}

        #endregion

        #region Commands
        public ICommand? GoBackCommand { get; set; }
        #endregion

        #region Constructor
        public MyProfileViewModel()
        {
            var instance = LocalDataService.GetInstance();
            if (instance != null) //If user is logged already
            {
                InitializeAsyncFunctions();
                //...initialization
            }
            else //If user isn't logged yet
            {
               //to assure that user can't access this page before logging in
            }
            GoBackCommand = new Command(GoBack);
        }
        public async void InitializeAsyncFunctions()
        {
            var instance = LocalDataService.GetInstance();
            User user = await instance.GetUserAsync();

            Username = user.UserName ?? "No username";
            Email = user.Email ?? "No email";
            RegDate = user.RegDate;

            List<Product>? products = await instance.GetProductsAsync();

            if (products != null)
            {
                FridgeName = "No name yet";
                ProductCount = products.Count;
                ExpiringSoonCount = await instance.GetExpinigSoonCountAsync();
                SpoiledCount = await instance.GetSpoiledCountAsync();
            }
        }
        #endregion

        #region Functions
        private void GoBack()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//MainPage");
            });
        }

        public async void UpdateFields()
        {
            FridgeName = await LocalDataService.GetInstance().GetFridgeNameAsync();
            ProductCount = await LocalDataService.GetInstance().GetProductCountAsync();
            User user = await LocalDataService.GetInstance().GetUserAsync();
            Username = user.UserName;
            Email = user.Email;
            RegDate = user.RegDate;
            ExpiringSoonCount = await LocalDataService.GetInstance().GetExpinigSoonCountAsync(); 
            SpoiledCount = await LocalDataService.GetInstance().GetSpoiledCountAsync();
        }
        #endregion
    }
}
