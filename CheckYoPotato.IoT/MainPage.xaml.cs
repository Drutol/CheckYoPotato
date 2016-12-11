using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CheckYoPotato.IoT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaCapture mediaCapture;
        private StorageFile photoFile;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";
        private bool isPreviewing;
        private bool isRecording;

        public MainPage()
        {
            this.InitializeComponent();

            InitializeCamera();
            
        }

        private async void InitializeCamera()
        {
            //Lel();
            IoTHubCommunicator ioTHubCommunicator = new IoTHubCommunicator();
            ioTHubCommunicator.MessageReceivedEvent += (sender, s) => TakePhoto();
            ioTHubCommunicator.ReceiveDataFromAzure();
            try
            {
                if (mediaCapture != null)
                {
                    // Cleanup MediaCapture object
                    if (isPreviewing)
                    {
                        await mediaCapture.StopPreviewAsync();
                        isPreviewing = false;
                    }
                    if (isRecording)
                    {
                        await mediaCapture.StopRecordAsync();
                        isRecording = false;
                    }
                    mediaCapture.Dispose();
                    mediaCapture = null;
                }

                Status.Text = "Initializing camera to capture audio and video...";
                // Use default initialization
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();

                // Set callbacks for failure and recording limit exceeded
                Status.Text = "Device successfully initialized for video recording!";
                //mediaCapture.Failed += new MediaCaptureFailedEventHandler(mediaCapture_Failed);
                //mediaCapture.RecordLimitationExceeded += new Windows.Media.Capture.RecordLimitationExceededEventHandler(mediaCapture_RecordLimitExceeded);

                // Start Preview                
                previewElement.Source = mediaCapture;
                await mediaCapture.StartPreviewAsync();
                isPreviewing = true;
                Status.Text = "Camera preview succeeded";

                await Task.Delay(3000);
                TakePhoto();
            }
            catch (Exception ex)
            {
                Status.Text = "Unable to initialize camera for audio/video mode: " + ex.Message;
            }
        }

        public async void TakePhoto()
        {
            try
            {
                photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                    PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName);
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
                await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);
                await UploadToBlob(photoFile, "fridge1", "photo.png");
                Status.Text = "Take Photo succeeded: " + photoFile.Path;
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
                //Cleanup();
            }
        }

        private async void Lel()
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, new Uri("https://checkyochat.azure-devices.net/devices/fridge/messages/devicebound?api-version=2016-02-03"));
            msg.Headers.Add("Authorization",
                "SharedAccessSignature sr=checkyochat.azure-devices.net&sig=nXi7Nur5xgNxgvoRyPH6GmtjCuFZqImdBnzlm%2fTfv4g%3d&se=1512959510&skn=iothubowner");
            var response = await new HttpClient().SendAsync(msg);

        }

        private async Task UploadToBlob(StorageFile path, string containerstr, string blob)
        {
            CloudStorageAccount storageAccount = GetAccount();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerstr);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            // Create the container if it doesn't already exist.


            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = await path.OpenReadAsync())
            {
                await blockBlob.UploadFromStreamAsync(fileStream.AsStream());
                
            }
            await Task.Delay(200);
            var msgSend = new HttpRequestMessage(HttpMethod.Post, new Uri("http://potatoesapi.azurewebsites.net/api/photos"));
            msgSend.Content =
                new StringContent(
                    "{\"camId\":0,\"link\":\"https://checkyopotato.blob.core.windows.net/fridge1/photo.png\",\"timestamp\":\"" +
                    DateTime.Now + "\"}");
            var respo = await new HttpClient().SendAsync(msgSend);
        }

        private CloudStorageAccount GetAccount()
        {
            return CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=checkyopotato;AccountKey=JxX4KUg8K3ubNufmHaI/q3PzNqHKSMCpua32atBVHP1iY6z95FxdgAZLDNOnzNjyXQyFgVR89x50AEi8hBeM4w==");
        }
    }
}
