using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CheckYoPotato.Web.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            var msg = new HttpRequestMessage(HttpMethod.Post, new Uri("https://checkyochat.azure-devices.net/devices/fridge/messages/events?api-version=2016-02-03"));
            msg.Headers.Add("Authorization", "SharedAccessSignature sr=checkyochat.azure-devices.net&sig=nXi7Nur5xgNxgvoRyPH6GmtjCuFZqImdBnzlm%2fTfv4g%3d&se=1512959510&skn=iothubowner");
            msg.Headers.Add("IoTHub-MessageId", "GimmePhoto");
            var resp = await new HttpClient().SendAsync(msg);


            var msgBack = new HttpRequestMessage(HttpMethod.Get, new Uri("http://potatoesapi.azurewebsites.net/api/photo"));
            var respBack = await new HttpClient().SendAsync(msgBack);
            var photo = JsonConvert.DeserializeObject<Photo>(await respBack.Content.ReadAsStringAsync());
            ImageLink = photo.Link;
            LoadingSpinnerVisibility = false;
        });

        
    }
}
