using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using SmartFridgeTracker.Views;
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

        //FullName
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        //Email
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

        //RegDate
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

        //Fridge Summary
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
        #endregion

        #region Commands
        public ICommand? GoBackCommand { get; set; }
        public ICommand? LogoutCommand { get; set; }
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
            LogoutCommand = new Command(Logout);
        }
        public async void InitializeAsyncFunctions()
        {
            UpdateFields();
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
        
        private void GoToLogin()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//LoginPage");
            });
        }

        public async void UpdateFields()
        {
            var instance = AppService.GetInstance();

            //User Details
            AuthUser? user = instance.fullDetailsLoggedInUser;
            if (user != null)
            {
                FullName = user.FullName ?? "No username";
                Email = user.Email ?? "No email";
                RegDate = user.RegDate;
                FridgeName = user.Fridge.Name;
            }

            //Fridge Details
            //List<Product>? products = await instance.GetProductsAsync();
            //if (products != null)
            //{
            //    FridgeName = "No name yet";
            //    ProductCount = products.Count;
            //    ExpiringSoonCount = await instance.GetExpinigSoonCountAsync();
            //    SpoiledCount = await instance.GetSpoiledCountAsync();
            //}      
        }

        private void Logout()
        {
            AppService.GetInstance().Logout();
            GoToLogin();
        }
        #endregion
    }
}
