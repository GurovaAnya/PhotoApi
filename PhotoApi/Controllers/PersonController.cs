using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoApi.Models;
using PhotoApi.Services;
using PhotoApi.Services.Interfaces;
using PhotoApi.ViewModels;

namespace PhotoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            return  Ok(await _personService.GetPeople());
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonViewModel>> GetPerson(int id)
        {
            var person = await _personService.GetPerson(id);

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, PersonViewModel person)
        {
            await _personService.PutPerson(id, person);

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonViewModel>> PostPerson(PersonViewModel personViewModel)
        {
            return await _personService.PostPerson(personViewModel);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonViewModel>> DeletePerson(int id)
        {
            var person = await _personService.DeletePerson(id);
            return person;
        }
    }
}
