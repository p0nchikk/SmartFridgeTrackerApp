using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFridgeTracker.Models;

namespace SmartFridgeTracker.Services
{
    internal class LocalDataService
    {
        private static LocalDataService? instance;
        private User? user;

        public static LocalDataService SetInstance()
        {
            instance = new LocalDataService();
            instance.user = new User();
            return instance;
        }

        public static LocalDataService? GetInstance()
        {
            return instance;
        }

        public bool AddUser(User user)
        {
            this.user = user;
            return true;
        }
        public User? GetUser()
        {
            return this.user;
        }
        public bool IsUserRegistered(string userName, string password)
        {
            if (user != null)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
