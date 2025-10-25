using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.ViewModels
{
    public class MyProfileViewModel : ViewModelBase
    {
        #region Variables Declaration

        // Identity
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
        #endregion

        #region Constructor
        public MyProfileViewModel()
        {
            if (LocalDataService.GetInstance()?.GetUser() != null) //If user is logged already
            {
                User user = LocalDataService.GetInstance().GetUser();

                Username = user.UserName ?? "No username";
                Email = user.Email ?? "No email";
                RegDate = user.RegDate;

                FridgeName = user.Fridge?.Name ?? "No Fridge";
                if (user.Fridge != null)
                {
                    ProductCount = user.Fridge.ProductsCount;
                    ExpiringSoonCount = user.Fridge?.ProductsList.Count(p => !p.IsSpoiled && p.ExpirationDate <= DateTime.Now.AddDays(3)) ?? 0;
                    SpoiledCount = user.Fridge?.ProductsList.Count(p => p.IsSpoiled) ?? 0;
                }

                LastUpdated = DateTime.Now; // Example, should be from last fridge update
                WeeklyUsage = 0; // Example, can be calculated from history
            }
            else //If user isn't logged yet
            {
                Username = "Polina";
                Email = "polina@gmail.com";
                RegDate = DateTime.Now;
                FridgeName = "Num-num";
                ProductCount = 10;
                ExpiringSoonCount = 2;
                SpoiledCount = 3;
                LastUpdated = DateTime.Now;
                WeeklyUsage = 4;
            }            
        }
        #endregion

        #region Functions
        #endregion
    }
}
