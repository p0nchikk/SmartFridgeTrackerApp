using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartFridgeTracker.Commands;
using SmartFridgeTracker.Services;

namespace SmartFridgeTracker.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        #region Variables Declaration 
        //Message

        private string? message;
        public string? Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange(nameof(Message));
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
            set { isPassword = value;
                OnPropertyChange();
            }
        }

        private string? viewPassIcon = "hide.png";
        public string? ViewPassIcon
        {
            get { return viewPassIcon; }
            set {
                if (value != null)
                    viewPassIcon = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Commands
        public ICommand LoginUserCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand ViewPassCommand { get; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            LoginUserCommand = new Command(LoginUser);
            GoToRegisterCommand = new Command(GoToRegister);
            ViewPassCommand = new Command(ViewPass);
        }
        #endregion

        #region Fuctions
        public void GoToRegister()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("//RegisterPage");
            });
        }
        public void ViewPass()
        {
            IsPassword = !IsPassword;
            if (!IsPassword) {
                ViewPassIcon = "view.png";
            }
            else
            {
                ViewPassIcon = "hide.png";
            }

        }

        public void LoginUser()
        {
            var instance = LocalDataService.GetInstance();

            if (instance == null)
            {
                Message = "User isn't registered yet";
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    Message = "Fill both the username and password";
                    return;
                }
                else
                {
                    if (Username == instance.GetUser().UserName && Password == instance.GetUser().Password)
                    {
                        Message = $"{Username}, welcome!";
                        // Navigation to the MainPage.xaml
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.GoToAsync("//MainPage");
                        });
                    }
                    else if (Password != instance.GetUser().Password)
                    {
                        Message = "Wrong password";
                    }
                    else // password is wrong
                    {
                        Message = "Please check your input";
                    }
                }
            }       
        }
        #endregion

    }
}
