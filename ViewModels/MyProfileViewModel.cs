using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
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
        private int achivements;
        public int Achivements
        {   
            get { return achivements; }
            set { achivements = value; }
        }

        private string? username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string? email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private DateTime regDate;
        public DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }


        // Fridge Summary
        private string? fridgeName;
        public string FridgeName
        {
            get { return fridgeName; }
            set { fridgeName = value; }
        }

        private int productCount;
        public int ProductCount
        {   
            get { return productCount; }
            set { productCount = value; }
        }

        private int expiringSoonCount;
        public int ExpiringSoonCount
        {
            get { return expiringSoonCount; }
            set { expiringSoonCount = value; }
        }

        private int spoiledCount;
        public int SpoiledCount
        {
            get { return spoiledCount; }
            set { spoiledCount = value; }
        }


        // Activity/Insights
        private DateTime lastUpdated;
        public DateTime LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }

        private int weeklyUsage;    
        public int WeeklyUsage
        {
            get { return weeklyUsage; }
            set { weeklyUsage = value; }
        }

        #endregion

        #region Commands
        public ICommand? GoBackCommand { get; set; }
        #endregion

        #region Constructor
        public MyProfileViewModel()
        {
            if (LocalDataService.GetInstance() != null) //If user is logged already
            {
                User user = LocalDataService.GetInstance().GetUser();

                Achivements = 0; //Temprorary
                Username = user.UserName ?? "No username";
                Email = user.Email ?? "No email";
                RegDate = user.RegDate;

                Fridge? fridge = user.Fridge;
                
                if (user.Fridge != null)
                {
                    FridgeName = fridge.Name ?? "No name yet";
                    ProductCount = fridge.ProductsCount;
                    ExpiringSoonCount = fridge?.ProductsList.Count(p => !p.IsSpoiled && p.ExpirationDate <= DateTime.Now.AddDays(3)) ?? 0;
                    SpoiledCount = fridge?.ProductsList.Count(p => p.IsSpoiled) ?? 0;
                }
                LastUpdated = DateTime.Now; // Example, should be from last fridge update
                WeeklyUsage = 0; // Example, can be calculated from history

            }
            else //If user isn't logged yet
            {
                Achivements = 0;
                Username = "Empty";
                Email = "Empty";
                RegDate = DateTime.Now;
                FridgeName = "Empty";
                ProductCount = 0;
                ExpiringSoonCount = 0;
                SpoiledCount = 0;
                LastUpdated = DateTime.Now;
                WeeklyUsage = 0;
            }         
            GoBackCommand = new Command(GoBack);
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
        #endregion
    }
}
