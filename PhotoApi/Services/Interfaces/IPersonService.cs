using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoApi.ViewModels;

namespace PhotoApi.Services.Interfaces
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
