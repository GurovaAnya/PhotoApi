using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using PhotoApi.Services;
using PhotoApi.Storage;
using PhotoApi.ViewModels;

namespace PhotoApi.Controllers
{
    [Route("api/person/{personId}/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly IFaceService _faceService;

        public FaceController(IFaceService faceService)
        {
            _faceService = faceService;
        }

        // GET: api/person/{personId}/Face
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FaceViewModel>>> GetFaces(int personId)
        {
            var faces = await _faceService.GetFaces(personId);
            return Ok(faces); 
        }

        
        // GET: api/person/{personId}/Face/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaceViewModel>> GetFace(int id, int personId)
        {
            var face = await _faceService.GetFace(id, personId);
            return new JsonResult(face);
        }

        // PUT: api/person/{personId}/Face/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFace(int id, FaceViewModel faceViewModel, int personId)
        {
            await _faceService.PutFace(id, faceViewModel, personId);
            return NoContent();
        }

        // POST: api/person/{personId}/Face
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FaceViewModel>> PostFace( [FromBody] FaceViewModel faceViewModel, int personId)
        {
            var face = await _faceService.PostFace(faceViewModel, personId);
            return face;
        }

        // DELETE: api/person/{personId}/Face/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FaceViewModel>> DeleteFace(int id, int personId)
        {
            var face = await _faceService.DeleteFace(id, personId);
            return face;
        }
    }
}
