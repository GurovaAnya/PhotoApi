using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Services
{
    public interface IFaceService
    {
        Task<IEnumerable<FaceViewModel>> GetFaces(int personId);
        Task<FaceViewModel> GetFace(int id, int personId);
        Task PutFace(int id, FaceViewModel faceViewModel, int personId);
        Task<FaceViewModel> PostFace(FaceViewModel faceViewModel, int personId);
        Task<FaceViewModel> DeleteFace(int id, int personId);
    }
}
