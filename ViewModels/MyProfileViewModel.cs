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
    public class MyProfileViewModel : ViewModelBase
    {
        #region Variables Declaration

        // Identity
        public int Achivements { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime RegDate { get; set; }

        // Fridge Summary
        public string? FridgeName { get; set; }
        public int ProductCount { get; set; }
        public int ExpiringSoonCount { get; set; }
        public int SpoiledCount { get; set; }

        // Activity/Insights
        public DateTime LastUpdated { get; set; }
        public int WeeklyUsage { get; set; }

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
