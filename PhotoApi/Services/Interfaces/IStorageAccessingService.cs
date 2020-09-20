using System.Threading.Tasks;

namespace PhotoApi.Services.Interfaces
{
    public interface IStorageAccessingService
    {
        Task<byte[]> Read(string facePhotoName);
        Task Delete(string facePhotoName);
        Task Write(byte[] photo, string path);
    }
}