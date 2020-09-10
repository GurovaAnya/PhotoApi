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
    [Route("api/find-face")]
    [ApiController]
    public class FindFaceController : ControllerBase
    {
        private readonly PhotoDbContext _context;

        public FindFaceController(PhotoDbContext context)
        {
            _context = context;
        }

        // GET: api/FindFace/5
        [HttpGet("{photo}")]
        public async Task<ActionResult<Person>> GetPerson(byte [] photo)
        {
            var person = await _context.Faces.Where(f => f.Photo == photo).Include(f => f.Person).ToListAsync() ;

            if (person == null)
            {
                return NotFound();
            }

            return new JsonResult(person);
        }
    }
}
