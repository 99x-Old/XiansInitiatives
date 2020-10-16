using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace XiansInitiatives.ServiceContract
{
    public interface IAzureBlobStorageService
    {
        string UploadFileToBlob(IFormFile file);
    }
}