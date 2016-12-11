using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace CheckYoPotato.IoT
{
    public class IoTHubCommunicator
    {
        public event EventHandler<string> MessageReceivedEvent;

        private string _iotHubConnectionString =
            "HostName=checkyochat.azure-devices.net;DeviceId=fridge;SharedAccessKey=vhMemlJ1vY4nr+jEPEr+25Z29EsWYILeYdsCOzISBfA=";

        public async Task SendDataToAzure(string message)
        {
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_iotHubConnectionString, TransportType.Http1);
            var msg = new Message(Encoding.UTF8.GetBytes(message));
            await deviceClient.SendEventAsync(msg);
        }


        public async void ReceiveDataFromAzure()
        {
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_iotHubConnectionString, TransportType.Http1);
            Message receivedMessage;
            string messageData;
            while (true)
            {
                receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage != null)
                {
                    messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    //this.OnMessageReceivedEvent(messageData);
                    MessageReceivedEvent.Invoke(this, null);
                    await deviceClient.CompleteAsync(receivedMessage);
                }
            }
        }


    }
}