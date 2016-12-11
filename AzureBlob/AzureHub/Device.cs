using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace AzureHub
{
    class Device
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "checkyochat.azure-devices.net";
        static string deviceKey = "vhMemlJ1vY4nr+jEPEr+25Z29EsWYILeYdsCOzISBfA=";
        public Device()
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("fridge", deviceKey));

            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
        private static async void SendDeviceToCloudMessagesAsync()
        {     
                var messageType = new
                {
                    makePhoto = "true",                    
                };
                var messageString = JsonConvert.SerializeObject(messageType);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
        }
    }
}
