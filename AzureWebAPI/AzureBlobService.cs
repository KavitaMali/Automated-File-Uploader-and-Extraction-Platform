using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace AzureWebAPI
{
    public class AzureBlobService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=kbmstorage;AccountKey=OOtGXjZb/7SgpsIc3bLImNZ+OAoSn/31gUIMIHyEgRbj6WukMXu7ASk7YtMsUsiVAZOJ1xUwh+sE+AStWUm/gQ==;EndpointSuffix=core.windows.net";
        public AzureBlobService()
        {
            _blobServiceClient = new BlobServiceClient(azureConnectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("kbmstoragecontainer");
        }

        public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
        {
            var azureResponse = new List<BlobContentInfo>();
            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    var client = await _blobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                    azureResponse.Add(client);
                }
            };
            return azureResponse;
        }
        public async Task<List<BlobItem>> GetUploadedBlob()
        {
            var items = new List<BlobItem>();
            var UploadedFiles = _blobContainerClient.GetBlobsAsync();
            await foreach (BlobItem file in UploadedFiles)
            {
                items.Add(file);
            }
            return items;
        }
    }

    
}
    