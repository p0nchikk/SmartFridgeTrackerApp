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
    public class RegisterViewModel : ViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange();
            }
        }
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
                OnPropertyChange(nameof(Password));
            }
        }
        private string verifyPassword;

        public string VerifyPassword
        {
            get { return verifyPassword; }
            set
            {
                verifyPassword = value;
                OnPropertyChange(nameof(VerifyPassword));
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

        public ICommand RegisterUser { get; }

        public ICommand GoToLogin { get; }

        public RegisterViewModel()
        {
            RegisterUser = new RegisterUserCommand(this);
            GoToLogin = new GoToLoginCommand();
        }
    }

    public class GoToLoginCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//LoginPage");
            });
        }
    }

    public class RegisterUserCommand : CommandBase
    {
        private readonly RegisterViewModel _vm;

        public RegisterUserCommand(RegisterViewModel vm)
        {
            _vm = vm;
        }

        public override void Execute(object parameter)
        {
            bool isValid = true;
            string message = "";

            // Username validation
            if (string.IsNullOrWhiteSpace(_vm.Username) || _vm.Username.Length < 5)
            {
                message += "Username too short, must be at least 5 chars.\n";
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
            if (_vm.Password != _vm.VerifyPassword)
            {
                message += "Passwords do not match.\n";
                isValid = false;
            }

            if (isValid)
            {
                // Create user and save to service
                User user = new User()
                {
                    UserName = _vm.Username,
                    Password = _vm.Password,
                    Email = _vm.Email,
                    RegDate = DateTime.Now
                };

                LocalDataService.GetInstance().AddUser(user);

                message = "Registration successful!";
                // Optionally navigate to Login page here

            }

            // Update ViewModel with validation messages
            _vm.Message = message;
        }
    }

}
