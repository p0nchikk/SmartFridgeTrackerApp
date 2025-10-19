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
        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChange(nameof(Message));
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
                OnPropertyChange();
            }
        }

        public ICommand LoginUser { get; }
        public ICommand GoToRegister { get; }

        public LoginViewModel()
        {
            LoginUser = new LoginUserCommand(this);
            GoToRegister = new GoToRegisterCommand();
        }

        public class GoToRegisterCommand : CommandBase
        {
            public override void Execute(object parameter)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync("//RegisterPage");
                });
            }
        }

        public class LoginUserCommand : CommandBase
        {
            private readonly LoginViewModel _vm;

            public LoginUserCommand(LoginViewModel vm)
            {
                _vm = vm;
            }

            public override void Execute(object parameter)
            {
                var user = LocalDataService.GetInstance().GetUser();
                
                if (!LocalDataService.IsInstanceAdded())
                {
                    _vm.Message = "User isn't registered yet";
                    return;
                }

                if (string.IsNullOrEmpty(_vm.Username) || string.IsNullOrEmpty(_vm.Password))
                {
                    _vm.Message = "Fill both the username and password";
                    return;
                }

                if (_vm.Username == user.UserName && _vm.Password == user.Password)
                {
                    _vm.Message = $"{_vm.Username}, welcome!";
                    // Navigation to the MainPage.xaml
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync("//MainPage");
                    });
                }
                else if (_vm.Username != user.UserName)
                {
                    _vm.Message = "Wrong username";
                }
                else // password is wrong
                {
                    _vm.Message = "Wrong password";
                }
            }
        }
    }
}
