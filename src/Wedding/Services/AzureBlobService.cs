using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Wedding.Services
{
    public class AzureBlobService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        private readonly string _blobKey;

        public AzureBlobService(IConfiguration configuration)
        {
            // Get the container name and connection string from the configuration
            _containerName = configuration.GetValue<string>("BlobContainerName") ?? "";
            string connectionString = configuration.GetValue<string>("BlobConnectionString") ?? "";

            _blobKey = connectionString.Split(new[] { "AccountKey=" }, StringSplitOptions.None)[1].Split(';')[0];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadFileAsync(Uri filePath, Stream fileContents)
        {
            // Get a reference to the blob container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Create the container if it does not exist
            await containerClient.CreateIfNotExistsAsync();

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(filePath.ToString());

            // Upload the file contents to the blob
            await blobClient.UploadAsync(fileContents, true);
        }

        public async Task<Uri> GetFileValet(Uri filePath)
        {
            // Get a reference to the blob container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(filePath.ToString());

            // Check if the blob exists
            if (await blobClient.ExistsAsync())
            {
                // Generate a SAS token for the blob
                string sasToken = GenerateSasToken(blobClient.Uri.AbsoluteUri, _blobKey);

                // Return the SAS URI for the blob
                return new Uri(blobClient.Uri + sasToken);
            }
            else
            {
                // Throw an exception if the blob does not exist
                throw new FileNotFoundException("The file does not exist in the storage.");
            }
        }

        public async Task<Stream> GetFile(Uri filePath)
        {
            // Get a reference to the blob container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(filePath.ToString());

            // Download the blob contents to a stream
            BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();

            // Return the stream
            return downloadInfo.Content;
        }

        public async Task DeleteFileAsync(Uri filePath)
        {
            // Get a reference to the blob container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(filePath.ToString());

            // Delete the blob if it exists
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task UpdateFileAsync(Uri filePath, Stream fileContents)
        {
            // Get a reference to the blob container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(filePath.ToString());

            // Check if the blob exists
            if (await blobClient.ExistsAsync())
            {
                // Upload the new file contents to the blob, overwriting the existing one
                await blobClient.UploadAsync(fileContents, true);
            }
            else
            {
                // Throw an exception if the blob does not exist
                throw new FileNotFoundException("The file does not exist in the storage.");
            }
        }


        public static string GenerateSasToken(string resourceUri, string key, string? policyName = null, int expiryInSeconds = 3600)
        {
            TimeSpan fromEpochStart = DateTime.UtcNow - new DateTime(1970, 1, 1);
            string expiry = Convert.ToString((int)fromEpochStart.TotalSeconds + expiryInSeconds);

            string stringToSign = WebUtility.UrlEncode(resourceUri) + "\n" + expiry;

            HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(key));
            string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));

            string token = string.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}", WebUtility.UrlEncode(resourceUri), WebUtility.UrlEncode(signature), expiry);

            if (!string.IsNullOrEmpty(policyName))
            {
                token += "&skn=" + policyName;
            }

            return token;
        }
    }
}
