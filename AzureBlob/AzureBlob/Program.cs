using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types


namespace AzureBlob
{
    class Program
    {

        private static void UploadToBlob(string path, string containerstr, string blob)
        {
            CloudStorageAccount storageAccount = GetAccount();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerstr);
            container.CreateIfNotExists();
            container.SetPermissions(
    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            // Create the container if it doesn't already exist.


            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(path))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
        private static void DownloadFromBlob(string path, string containerstr, string blob)
        {
            CloudStorageAccount storageAccount = GetAccount();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerstr);
            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob);
                        // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(path))
            {
                blockBlob.DownloadToStream(fileStream);
            }
            //HttpWebRequest
            
        }
        private static CloudStorageAccount GetAccount()
        {
            return CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }
        static void Main(string[] args)
        {
            UploadToBlob(@"..\..\raspi.png", "fridge1", "photo.png");
            DownloadFromBlob(@"..\..\raspi3.png", "fridge1", "photo.png");

            // Loop over items within the container and output the length and URI.
            /*foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }*/
            Console.ReadKey();
        }
    }
}
