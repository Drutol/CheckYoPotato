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
        public event NavigationRequest NavigationRequested;

        public void Navigate(PageIndex page)
        {
            NavigationRequested?.Invoke(page);
        }
    }
}
