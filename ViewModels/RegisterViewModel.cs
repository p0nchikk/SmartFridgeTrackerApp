using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridgeTracker.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChange();
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChange();
            }
        }
        private string verifyPassword;

        public string Verifypassword
        {
            get { return verifyPassword; }
            set
            {
                verifyPassword = value;
                OnPropertyChange();
            }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChange();
            }
        }


        public RegisterViewModel()
        {

        }
    }
}
