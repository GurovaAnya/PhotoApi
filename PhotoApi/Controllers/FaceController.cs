using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;

namespace PhotoApi.Controllers
{
    [Route("api/person/{personId}/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly PhotoDbContext _context;

        public FaceController(PhotoDbContext context)
        {
            _context = context;
        }

        // GET: api/person/{personId}/Face
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Face>>> GetFaces(int personId)
        {
            return await _context.Faces.Where(f=>f.PersonId==personId).ToListAsync();
        }

        // GET: api/person/{personId}/Face/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Face>> GetFace(int id, int personId)
        {
            var face = await _context.Faces.Where(f => f.PersonId == personId && f.Id==id).ToListAsync();

            if (face == null)
            {
                return NotFound();
            }

            return new JsonResult(face);
        }

        // PUT: api/person/{personId}/Face/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFace(int id, Face face, int personId)
        {
            if (id != face.Id)
            {
                return BadRequest();
            }

            _context.Entry(face).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Face>> PostFace(Face face, int personId)
        {
            face.PersonId = personId;
            _context.Faces.Add(face);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFace", new { id = face.Id }, face);
        }

        // DELETE: api/person/{personId}/Face/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Face>> DeleteFace(int id)
        {
            var face = await _context.Faces.FindAsync(id);
            if (face == null)
            {
                return NotFound();
            }

            _context.Faces.Remove(face);
            await _context.SaveChangesAsync();

            return face;
        }

        private bool FaceExists(int id)
        {
            return _context.Faces.Any(e => e.Id == id);
        }
    }
}
