using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonViewModel>> GetPeople();
        Task<PersonViewModel> GetPerson(int id);
        Task PutPerson(int id, PersonViewModel personViewModel);
        Task<PersonViewModel> PostPerson(PersonViewModel personViewModel);
        Task<PersonViewModel> DeletePerson(int id);
    }
}
