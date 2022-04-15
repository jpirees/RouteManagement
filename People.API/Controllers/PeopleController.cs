using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using People.API.Services;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleService _peopleService;

        public PeopleController(PeopleService peopleService) => _peopleService = peopleService;


        [HttpGet]
        public async Task<IEnumerable<Person>> Get() => 
            await _peopleService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPerson")]
        public async Task<ActionResult<dynamic>> Get(string id)
        {
            var person = await _peopleService.GetById(id);

            if (person == null)
                return NotFound(new {
                    statusCode = 404,
                    message = "Person not found"
                });

            return person;
        }

        [HttpGet("{name}")]
        public async Task<dynamic> GetByName(string name)
        {
            var person = await _peopleService.GetByName(name);

            if (person == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person not found"
                });

            return person;
        }


        [HttpPost]
        public async Task<dynamic> Post([FromBody] Person personIn)
        {
            var person = await _peopleService.Create(personIn);

            return CreatedAtRoute("GetPerson", new { id = person.Id }, person);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<dynamic> Update(string id, [FromBody] Person personIn)
        {
            var person = await _peopleService.Update(id, personIn);

            if (person == null)
                return NotFound(new {
                    statusCode = 404,
                    message = "Person not found"
                });

            return NoContent();
        }


        [HttpPut("{id:length(24)}/Status")]
        public async Task<dynamic> UpdateStatus(string id)
        {
            var person = await _peopleService.UpdateStatus(id);

            if (person == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person not found"
                });

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<dynamic> Delete(string id)
        {
            var person = await _peopleService.Remove(id);

            if (person == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Person not found"
                });

            return NoContent();
        }
    }
}
