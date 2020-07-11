using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;


namespace BookShelfAPI.Storage
{
    public class AzureBlobStorage
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _containerName;

        static BlobContainerClient _container;
        public AzureBlobStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("StorageConnectionString");
            _containerName = _configuration.GetValue<string>("ContainerName");
            _container = new BlobContainerClient(_connectionString, _containerName);
        }
        public AzureBlobStorage()
        {
            
        }
        
        public async Task<string> UploadFile(Stream stream, string fileName)
        {
            await _container.CreateIfNotExistsAsync();
            await _container.SetAccessPolicyAsync(PublicAccessType.Blob);
            BlobClient blob = _container.GetBlobClient(fileName);
          
            if (!blob.Exists())
            {
                await blob.UploadAsync(stream);
            }
            return blob.Uri.ToString();
        }

        public async void RemoveFile(string fileName)
        {
            BlobClient blob = _container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }
    }
}