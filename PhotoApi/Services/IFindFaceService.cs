using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Services
{
    public interface IFindFaceService
    {
        Task<IEnumerable<PersonViewModel>> FindFace(byte[] photo);
    }
}
