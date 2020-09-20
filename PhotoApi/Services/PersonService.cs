using Microsoft.EntityFrameworkCore;
using PhotoApi.Exceptions;
using PhotoApi.Models;
using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoApi.Services.Interfaces;

namespace PhotoApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly PhotoDbContext _context;

        public PersonService(PhotoDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PersonViewModel>> GetPeople()
        {
            return await _context.People.Select(p => MapToViewModel(p)).ToListAsync();
        }

        public async Task<PersonViewModel> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                throw new NotFoundException("Человека с такими параметрами не найдено");
            }

            return MapToViewModel(person);
        }

        public async Task PutPerson(int id, PersonViewModel personViewModel)
        {
            if (id != personViewModel.Id)
            {
                throw new BadRequestException("Данные запроса не совпадают");
            }

            var person = MapToModel(personViewModel);
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    throw new NotFoundException("Человека с такими параметрами не найдено");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<PersonViewModel> PostPerson(PersonViewModel personViewModel)
        {
            var person = MapToModel(personViewModel);
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return personViewModel;
        }

        public async Task<PersonViewModel> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                throw new NotFoundException("Человека с такими параметрами не найдено");
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return MapToViewModel(person);
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

        private static PersonViewModel MapToViewModel(Person person)
        {
            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Patronymic = person.Patronymic
            };
        }

        private static Person MapToModel(PersonViewModel personViewModel)
        {
            return new Person
            {
                Id = personViewModel.Id,
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName,
                Patronymic = personViewModel.Patronymic
            };
        }
    }
}
