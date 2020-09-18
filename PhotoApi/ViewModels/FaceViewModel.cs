using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.ViewModels
{
    public class FaceViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public byte[] Photo { get; set; }
    }
}
