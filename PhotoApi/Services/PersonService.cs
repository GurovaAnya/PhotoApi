using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using PhotoApi.Storage;
using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Services
{
    public class PersonService
    {
        private PhotoDbContext _context;

        public PersonService(PhotoDbContext context)
        {
            _context = context;
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

        private PersonViewModel MapToViewModel(Person person)
        {
            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Patronymic = person.Patronymic
            };
        }
    }
}