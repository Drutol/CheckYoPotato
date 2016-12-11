using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckYoPotato.Models.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CheckYoPotato.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                RaisePropertyChanged(() => Loading);
            }
        }

        public RelayCommand LoginCommand => new RelayCommand(async () =>
        {
            Loading = true;
            await Task.Delay(1000);
            Loading = false;
            ViewModelLocator.MainViewModel.Navigate(PageIndex.PagePhotos);
        });
    }
}
