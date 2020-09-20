using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using PhotoApi.Services;
using PhotoApi.Services.Interfaces;
using PhotoApi.Storage;
using PhotoApi.ViewModels;

namespace PhotoApi.Controllers
{
    [Route("api/find-face")]
    [ApiController]
    public class FindFaceController : ControllerBase
    {
        private readonly IFindFaceService _findFaceService;

        public FindFaceController(IFindFaceService findFaceService)
        {
            _findFaceService = findFaceService;
        }

        // GET: api/find-face
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonViewModel>>> FindFace([FromBody] byte [] photo)
        {
            var people = await _findFaceService.FindFace(photo);
            return Ok(people);
        }
    }
}
