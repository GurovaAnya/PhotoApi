using Microsoft.EntityFrameworkCore;
using PhotoApi.Exceptions;
using PhotoApi.Models;
using PhotoApi.Storage;
using PhotoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoApi.Services
{
    public class FindFaceService : IFindFaceService
    {
        private readonly PhotoDbContext _context;
        private readonly GoogleStorage _googleStorage;

        public FindFaceService(PhotoDbContext context, GoogleStorage googleStorage)
        {
            _context = context;
            _googleStorage = googleStorage;
        }

        public async Task<IEnumerable<PersonViewModel>> FindFace(byte[] photo)
        {
            int hash = Face.CreateHash(photo);
            var faces = await _context.Faces.Where(f => f.PhotoHash == hash).Include(f => f.Person).ToListAsync();

            if (faces.Count == 0)
            {
                throw new NotFoundException();
            }

            if (faces.Count == 1)
            {
                return new PersonViewModel[] {
                    new PersonViewModel
                    {
                        Id = faces[0].Person.Id,
                        FirstName = faces[0].Person.FirstName,
                        LastName = faces[0].Person.LastName,
                        Patronymic = faces[0].Person.Patronymic
                    }
                };
            }

            // Проверка коллизий
            Dictionary<int, PersonViewModel> people = new Dictionary<int, PersonViewModel>();

            foreach (Face face in faces)
            {
                if (people.ContainsKey(face.PersonId))
                    continue;
                var storagePhoto = await _googleStorage.Read(face.PhotoName);
                if (storagePhoto.SequenceEqual(photo))
                {
                    var personViewModel = new PersonViewModel()
                    {
                        Id = face.Person.Id,
                        FirstName = face.Person.FirstName,
                        LastName = face.Person.LastName,
                        Patronymic = face.Person.Patronymic
                    };

                    people.Add(face.PersonId, personViewModel);
                }
            }
            return people.Values;
        }
    }
}
