using SmartFridgeTracker.Models;
using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.ViewModels
{
    internal class MyProfileViewModel : ViewModelBase
    {
        private string username;
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChange(); }
        }

        private string email;
        public string Email
        {
            get => email;
            set { email = value; OnPropertyChange(); }
        }

        private string fridgeName;
        public string FridgeName
        {
            get => fridgeName;
            set { fridgeName = value; OnPropertyChange(); }
        }

        private int productCount;
        public int ProductCount
        {
            get => productCount;
            set { productCount = value; OnPropertyChange(); }
        }
        private DateTime regDate;
            
        public DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        public MyProfileViewModel()
        {
            if ( LocalDataService.GetInstance() != null)
            {
                User user = LocalDataService.GetInstance().GetUser();
                Username = user.UserName ?? "Name is absent";
                Email = user.Email ?? "Email is absent";
                if (user.Fridge != null)
                {
                    FridgeName = user.Fridge.Name ?? "Fridge Name is absent";
                    ProductCount = user.Fridge.ProductsCount;
                }
                RegDate = user.RegDate;
            }
        }
    }
}
