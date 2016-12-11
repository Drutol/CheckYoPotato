using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            msg.Content = new StringContent("dsfdsfdsf");
            var resp = await new HttpClient().SendAsync(msg);

            //var lolololololololololololoo = new IoTHubCommunicator();
            //await lolololololololololololoo.SendDataToAzure("fdoikgdfikjgnikjudsfngsfdxikjugikljudfngikjudfgjuikl");

            await Task.Delay(3000);
            ImageLink = "https://checkyopotato.blob.core.windows.net/fridge1/photo.png";
            LoadingSpinnerVisibility = false;
        });

        
    }
}
