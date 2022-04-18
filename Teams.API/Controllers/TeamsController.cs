using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Teams.API.Services;

namespace Teams.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsService _teamsService;

        public TeamsController(TeamsService teamsService) => _teamsService = teamsService;


        [HttpGet]
        public async Task<IEnumerable<Team>> Get() =>
            await _teamsService.Get();


        [HttpGet("{id:length(24)}", Name = "GetTeam")]
        public async Task<dynamic> Get(string id)
        {
            var team = await _teamsService.GetById(id);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return team;
        }

        [HttpGet("{name}")]
        public async Task<dynamic> GetByName(string name)
        {
            var team = await _teamsService.GetByName(name);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return team;
        }


        [HttpPost]
        public async Task<dynamic> Post([FromBody] Team teamIn)
        {
            var teamExist = await _teamsService.GetByName(teamIn.Name);

            if (teamExist != null)
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Team name is already registered"
                });

            await _teamsService.Create(teamIn);

            return CreatedAtRoute("GetTeam", new { id = teamIn.Id }, teamIn);
        }


        [HttpPut("{id:length(24)}")]
        public async Task<dynamic> Update(string id, [FromBody] Team teamIn)
        {
            var team = await _teamsService.GetById(id);

            if (!team.Name.Equals(teamIn.Name))
            {
                var teamExist = await _teamsService.GetByName(teamIn.Name);

                if (teamExist != null)
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Team name is already registered"
                    });
            }

            teamIn.People = team.People;

            var response = await _teamsService.Update(id, teamIn);

            if (response == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });


            return NoContent();
        }


        [HttpPut("{id:length(24)}/Status")]
        public async Task<dynamic> UpdateStatus(string id)
        {
            var team = await _teamsService.UpdateStatus(id);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return NoContent();
        }

        [HttpPut("{id:length(24)}/Insert")]
        public async Task<dynamic> UpdateInsert(string id, [FromBody] Person person)
        {
            var team = await _teamsService.UpdateToInsert(id, person);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return NoContent();
        }


        [HttpPut("{id:length(24)}/Remove")]
        public async Task<dynamic> UpdateRemove(string id, [FromBody] Person person)
        {
            var team = await _teamsService.UpdateToRemove(id, person);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<dynamic> Delete(string id)
        {
            var team = await _teamsService.Remove(id);

            if (team == null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Team not found"
                });

            return NoContent();
        }
    }
}
