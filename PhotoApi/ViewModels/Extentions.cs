using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.ViewModels
{
    public static class Extentions
    {
        public static async Task<IEnumerable<FaceViewModel>> WhenAll<FaceViewModel>(this IEnumerable<Task<FaceViewModel>> tasks)
        {
            return await Task.WhenAll(tasks);
        }
    }
}
