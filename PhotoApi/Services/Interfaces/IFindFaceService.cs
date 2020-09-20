using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoApi.ViewModels;

namespace PhotoApi.Services.Interfaces
{
    public interface IFindFaceService
    {
        Task<IEnumerable<PersonViewModel>> FindFace(byte[] photo);
    }
}
