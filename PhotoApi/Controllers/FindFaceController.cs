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

        // GET: api/find-face
        [HttpGet]
        public async Task<ActionResult<Person>> FindFace([FromBody] string photo)
        {
            var person = await _context.Faces.Where(f => f.Photo == photo).Select(f => f.Person).ToListAsync();

            if (person.Count == 0)
            {
                return NotFound();
            }

            return new JsonResult(person);
        }
    }
}
