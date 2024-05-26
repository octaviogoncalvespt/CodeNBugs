using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Repo.Services
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IConfiguration _configuration;
        public UploadImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadImageAsync(IFormFile formFile, string containerName)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var connectionString = _configuration.GetConnectionString("AzureBlobStorageConnection");
                
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

                string blobName = Guid.NewGuid().ToString() + formFile.FileName;
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                using (var stream = formFile.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                return blobName;
            }

            throw new ArgumentException("formFile is null or empty");
        }


        public async Task<bool> DeleteImageAsync(string blobName, string containerName)
        {
            var connectionString = _configuration.GetConnectionString("AzureBlobStorageConnection");

            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync();
        }

    }
}
