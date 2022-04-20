using System.Collections.Generic;
using System.Threading.Tasks;
using Cities.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Cities.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesService _citiesService;

        public CitiesController(CitiesService citiesService) => _citiesService = citiesService;


        [HttpGet]
        public async Task<IEnumerable<City>> Get() =>
            await _citiesService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCity")]
        public async Task<ActionResult<dynamic>> Get(string id)
        {
            var city = await _citiesService.GetById(id);

            if (city == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "City not found"
                });

            return city;
        }

        [HttpGet("{name}")]
        public async Task<dynamic> GetByName(string name)
        {
            var city = await _citiesService.GetByName(name);

            if (city == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "City not found"
                });

            return city;
        }

        [HttpGet("{state}/Cities")]
        public async Task<dynamic> GetCitiesByState(string state)
        {
            var cities = await _citiesService.GetCitiesByState(state);

            if (cities == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Cities not found"
                });

            return cities;
        }

        [HttpPost]
        public async Task<dynamic> Post([FromBody] City cityIn)
        {
            var city = await _citiesService.Create(cityIn);

            return CreatedAtRoute("GetCity", new { id = city.Id }, city);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<dynamic> Update(string id, [FromBody] City cityIn)
        {
            var city = await _citiesService.Update(id, cityIn);

            if (city == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "City not found"
                });

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<dynamic> Delete(string id)
        {
            var city = await _citiesService.Remove(id);

            if (city == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "City not found"
                });

            return NoContent();
        }
    }
}
