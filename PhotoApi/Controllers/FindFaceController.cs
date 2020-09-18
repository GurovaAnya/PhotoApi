using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using PhotoApi.Storage;
using PhotoApi.ViewModels;

namespace PhotoApi.Controllers
{
    [Route("api/find-face")]
    [ApiController]
    public class FindFaceController : ControllerBase
    {
        private readonly PhotoDbContext _context;
        private readonly GoogleStorage _googleStorage;

        public FindFaceController(PhotoDbContext context)
        {
            _context = context;
            _googleStorage = new GoogleStorage("ivory-plane-277612", "ivory-plane-277612-bucket");
        }

        // GET: api/find-face
        [HttpGet]
        public async Task<ActionResult<PersonViewModel>> FindFace([FromBody] byte [] photo)
        {
            int hash = Face.CreateHash(photo);
            var faces = await _context.Faces.Where(f => f.PhotoHash == hash).Include(f => f.Person).ToListAsync();

            if (faces.Count == 0)
            {
                return NotFound();
            }

            if (faces.Count == 1)
            {
                return new PersonViewModel
                {
                    Id = faces[0].Person.Id,
                    FirstName = faces[0].Person.FirstName,
                    LastName = faces[0].Person.LastName,
                    Patronymic = faces[0].Person.Patronymic
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
                    var personVM = new PersonViewModel()
                    {
                        Id = face.Person.Id,
                        FirstName = face.Person.FirstName,
                        LastName = face.Person.LastName,
                        Patronymic = face.Person.Patronymic
                    };

                    people.Add(face.PersonId, personVM);
                }
            }
            return new JsonResult(people.Values);
        }
    }
}
