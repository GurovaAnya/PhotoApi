using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using PhotoApi.Services.Interfaces;

namespace PhotoApi.Services
{
    public class GoogleStorageService : IDisposable, IStorageAccessingService
    {
        private readonly string _bucketName;
        private readonly StorageClient _storageClient;

        public GoogleStorageService()
        {
            this._bucketName = "ivory-plane-277612-bucket";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Directory.GetCurrentDirectory() + "/appsettings.json");
            var credential = GoogleCredential.GetApplicationDefault();
            _storageClient = StorageClient.Create();
        }

        public async Task Write(byte[] photo, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(photo, 0, photo.Length);
                var cloudFile = await _storageClient.UploadObjectAsync(_bucketName, fileName, "application/octet-stream", stream);
            }
        }        

        public async Task<byte[]> Read(string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await _storageClient.DownloadObjectAsync(_bucketName, fileName, stream);
                return stream.ToArray();
            }
        }

        public async Task Delete(string fileName)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, fileName);
        }

        public void Dispose()
        {
            _storageClient.Dispose();
        }
    }
}
