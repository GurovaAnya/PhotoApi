using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using System.IO;
using System.Text;
using System.Text.Json;

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
        //[HttpGet("{photo}")]
        [HttpGet]
        public async Task<ActionResult<Person>> GetPerson([FromBody]string photo)
        {
            var person = await _context.Faces.Where(f => f.Photo ==photo).Select(f => f.Person).Include(p=>p.Faces).ToListAsync();

            if (person == null)
            {
                return NotFound();
            }

            return new JsonResult(person);
        }
    }
}
