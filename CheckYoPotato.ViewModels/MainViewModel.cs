using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckYoPotato.Models.Delegates;
using CheckYoPotato.Models.Enums;
using GalaSoft.MvvmLight;

namespace CheckYoPotato.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _currentStatus;
        public event NavigationRequest NavigationRequested;

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set
            {
                _currentStatus = value;
                RaisePropertyChanged(() => CurrentStatus);
            }
        }

        public void Navigate(PageIndex page)
        {
            switch (page)
            {
                case PageIndex.PageSplash:
                    CurrentStatus = "Connecting to fridgeverse!";
                    break;
                case PageIndex.PageLogin:
                    CurrentStatus = "Login to Yo Fridge";
                    break;
                case PageIndex.PageFridge:
                    CurrentStatus = "Yo Fridge";
                    break;
                case PageIndex.PageFridgeChat:
                    CurrentStatus = "Yo Fridge Chat";
                    break;
                case PageIndex.PagePhotos:
                    CurrentStatus = "Yo Fridge Photos";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
            NavigationRequested?.Invoke(page);
        }
    }
}
