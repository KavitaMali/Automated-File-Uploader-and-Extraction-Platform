using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace AzureWebAPI
{
    public class AzureBlobService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        
        public AzureBlobService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            _blobServiceClient = new BlobServiceClient(connectionString);
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
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
    