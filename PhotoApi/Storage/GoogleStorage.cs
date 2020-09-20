using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace PhotoApi.Storage
{
    public class GoogleStorage
    {
        string projectId;
        string bucketName;
        private StorageClient _storageClient;

        public GoogleStorage()
        {
            this.projectId = "ivory-plane-277612";
            this.bucketName = "ivory-plane-277612-bucket";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Directory.GetCurrentDirectory() + "/appsettings.json");
            var credential = GoogleCredential.GetApplicationDefault();
            _storageClient = StorageClient.Create();
        }

        public async Task Write(byte[] photo, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(photo, 0, photo.Length);
                var cloudFile = await _storageClient.UploadObjectAsync(bucketName, fileName, "application/octet-stream", stream);
            }
        }        

        public async Task<byte[]> Read(string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await _storageClient.DownloadObjectAsync(bucketName, fileName, stream);
                return stream.ToArray();
            }
        }

        public async Task Delete(string fileName)
        {
            await _storageClient.DeleteObjectAsync(bucketName, fileName);
        }
    }
}
