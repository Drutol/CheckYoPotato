using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CheckYoPotato.ViewModels
{
    public class PhotosPageViewModel : ViewModelBase
    {
        private string _imageLink;
        private bool _loadingSpinnerVisibility;

        public string ImageLink
        {
            get { return _imageLink; }
            set
            {
                _imageLink = value;
                RaisePropertyChanged(() => ImageLink);
            }
        }

        public bool LoadingSpinnerVisibility
        {
            get { return _loadingSpinnerVisibility; }
            set
            {
                _loadingSpinnerVisibility = value;
                RaisePropertyChanged(() => LoadingSpinnerVisibility);
            }
        }

        public void NavigatedTo()
        {
            RefreshPhotoCommand.Execute(null);
        }

        public RelayCommand RefreshPhotoCommand => new RelayCommand( async () =>
        {
            LoadingSpinnerVisibility = true;
            await Task.Delay(1000);
            ImageLink = "https://checkyopotato.blob.core.windows.net/fridge1/photo.png";
            LoadingSpinnerVisibility = false;
        });
    }
}
