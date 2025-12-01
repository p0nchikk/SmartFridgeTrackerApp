using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartFridgeTracker.ViewModels
{
    public class ProductInfoViewModel : ViewModelBase
    {
        #region Variables Declaration
        #endregion

        #region Commands
        public ICommand? GoBackCommand {  get; set; }
        #endregion

        #region Constructor
        public ProductInfoViewModel() 
        { 
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
