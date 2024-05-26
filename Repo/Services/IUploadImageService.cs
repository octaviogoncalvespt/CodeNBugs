using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface IUploadImageService
    {
        Task<string> UploadImageAsync(IFormFile formFile, string containerName);

        Task<bool> DeleteImageAsync(string blobName, string containerName);
    }
}
