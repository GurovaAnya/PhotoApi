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
    [Route("api/person/{personId}/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly PhotoDbContext _context;
        private readonly GoogleStorage _googleStorage;

        public FaceController(PhotoDbContext context)
        {
            _context = context;
            _googleStorage = new GoogleStorage("ivory-plane-277612", "ivory-plane-277612-bucket");
        }

        // GET: api/person/{personId}/Face
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Face>>> GetFaces(int personId)
        {
            var face = await _context.Faces.Where(f=>f.PersonId==personId).ToListAsync();
            return face;
        }

        // GET: api/person/{personId}/Face/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaceViewModel>> GetFace(int id, int personId)
        {
            var face = await _context.Faces.Where(f => f.PersonId == personId && f.Id == id).SingleAsync();
            if (face == null)
            {
                return NotFound();
            }

            // Получение фото из облака
            var photo = await _googleStorage.Read(face.PhotoName);

            var faceViewModel = new FaceViewModel() { Id = face.Id, PersonId = personId, Photo = photo };
            return faceViewModel;
        }

        // PUT: api/person/{personId}/Face/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFace(int id, FaceViewModel faceVM, int personId)
        {
            if (id != faceVM.Id)
            {
                return BadRequest();
            }

            var face = await _context.Faces.Where(f => f.Id == id).SingleAsync();

            if (personId != face.PersonId)
            {
                return BadRequest();
            }

            face.PersonId = faceVM.PersonId;


            int photoHash = Face.CreateHash(faceVM.Photo);
            string oldName = null;
            // Если фото поменялось
            if (photoHash != face.PhotoHash)
            {
                oldName = face.PhotoName;
                face.PhotoName = DateTime.UtcNow.Ticks.ToString();
                face.PhotoHash = photoHash;
            }

            _context.Entry(face).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                if (oldName != null)
                {
                    await _googleStorage.Delete(oldName);
                    await _googleStorage.Write(faceVM.Photo, face.PhotoName);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/person/{personId}/Face
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FaceViewModel>> PostFace( [FromBody] FaceViewModel faceVM, int personId)
        {
            if (personId != faceVM.PersonId) 
            {
                return BadRequest();
            }

            string path = DateTime.UtcNow.Ticks.ToString();
            await _googleStorage.Write(faceVM.Photo, path);
            var face = new Face() 
            {
                Id = faceVM.Id,
                PersonId = faceVM.PersonId,
                PhotoHash = Face.CreateHash(faceVM.Photo),
                PhotoName = path
            };
            _context.Faces.Add(face);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFace", new { id = face.Id, personId = face.PersonId }, face);
        }

        // DELETE: api/person/{personId}/Face/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FaceViewModel>> DeleteFace(int id, int personId)
        {
            var face = await _context.Faces.FindAsync(id);
            if (face == null)
            {
                return NotFound();
            }

            if (face.PersonId != personId)
            {
                return BadRequest();
            }

            var photo = await _googleStorage.Read(face.PhotoName);
            _context.Faces.Remove(face);
            await _context.SaveChangesAsync();
            await _googleStorage.Delete(face.PhotoName);

            return new FaceViewModel() {Id = face.Id, PersonId = face.PersonId, Photo = photo};
        }

        private bool FaceExists(int id)
        {
            return _context.Faces.Any(e => e.Id == id);
        }
    }
}
