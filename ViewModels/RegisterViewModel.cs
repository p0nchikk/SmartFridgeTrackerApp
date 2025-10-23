using SmartFridgeTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartFridgeTracker.Commands;
using SmartFridgeTracker.Models;
using System.Text.RegularExpressions;


namespace SmartFridgeTracker.ViewModels
{
    #region Variables Declaration
    public class RegisterViewModel : ViewModelBase
    {
        //Message

        private string? message;
        public string? Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange();
            }
        }

        //Username

        private string? username;
        public string? Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChange();
            }
        }

        //Password

        private string? password;
        public string? Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChange();
            }
        }

        private bool isPassword = true;
        public bool IsPassword
        {
            get { return isPassword; }  
            set {
                isPassword = value;
                OnPropertyChange();
            }
        }

        private string viewPassIcon = "hide.png";
        public string ViewPassIcon
        {
            get { return viewPassIcon; }
            set {
                if ( value != null )
                viewPassIcon = value;
                OnPropertyChange();
            }
        }

        //Password Verification

        private string? verifyPassword;
        public string? VerifyPassword
        {
            get { return verifyPassword; }
            set
            {
                verifyPassword = value;
                OnPropertyChange(nameof(VerifyPassword));
            }
        }

        private bool isVerifyPassword = true;
        public bool IsVerifyPassword
        {
            get { return isVerifyPassword; }
            set
            {
                isVerifyPassword = value;
                OnPropertyChange();
            }
        }

        private string viewVerifyPassIcon = "hide.png";
        public string ViewVerifyPassIcon
        {
            get { return viewVerifyPassIcon; }
            set
            {
                if (value != null)
                    viewVerifyPassIcon = value;
                OnPropertyChange();
            }
        }

        //Email

        private string? email;
        public string? Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChange();
            }
        }

        #endregion

    #region Commands
        public ICommand CommandRegisterUser { get; }
        public ICommand CommandGoToLogin { get; }
        public ICommand CommandViewPass { get; }
        public ICommand CommandViewVerifyPass { get; }
        #endregion

    #region Constructor
        public RegisterViewModel()
        {
            CommandRegisterUser = new Command(RegisterUser);
            CommandGoToLogin = new Command(GoToLogin);
            CommandViewPass = new Command(ViewPass);
            CommandViewVerifyPass = new Command(ViewVerifyPass);
        }
        #endregion

    #region Functions
        public void GoToLogin()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//LoginPage");
            });
        }
        public void ViewPass()
        {
            IsPassword = !IsPassword;
            if (!IsPassword)
            {
                ViewPassIcon = "view.png";
            }
            else
            {
                ViewPassIcon = "hide.png";
            }
        }
        public void ViewVerifyPass()
        {
            IsVerifyPassword = !IsVerifyPassword;
            if (!IsVerifyPassword)
            {
                ViewVerifyPassIcon = "view.png";
            }
            else
            {
                ViewVerifyPassIcon = "hide.png";
            }
        }
        public void RegisterUser()
        {
            bool isValid = true;

            // Username validation
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 5)
            {
                Message += "Username too short, must be at least 5 chars.\n";
                isValid = false;
            }

            // Password validation (optional regex)
            //string passPattern = @"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?:{}|<>])[A-Za-z\d!@#$%^&*(),.?:{}|<>]{8,}$";
            //bool isPasswordOk = Regex.IsMatch(_vm.Password ?? "", passPattern);
            //if (!isPasswordOk)
            //{
            //    message += "Password must be at least 8 chars, contain uppercase and special char.\n";
            //    isValid = false;
            //}

            // Email validation (optional regex)
            //string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            //bool isEmailOk = Regex.IsMatch(_vm.Email ?? "", emailPattern);
            //if (!isEmailOk)
            //{
            //    message += "Enter a valid email address.\n";
            //    isValid = false;
            //}

            // Password confirmation
            if (Password != VerifyPassword)
            {
                Message += "Passwords do not match.\n";
                isValid = false;
            }

            if (isValid)
            {
                // Create user and save to service
                User user = new User()
                {
                    UserName = Username,
                    Password = Password,
                    Email = Email,
                    RegDate = DateTime.Now,
                    Fridge = new Fridge()
                };

                LocalDataService.SetInstance().AddUser(user);

                Message = "Registration successful!";

                // Optionally navigate to Login page here

            }
        }
        #endregion

    }

}
