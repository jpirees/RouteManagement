using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;
using Teams.API.Configuration;

namespace Teams.API.Services
{
    public class TeamsService
    {
        public IMongoCollection<Team> _teams;

        public TeamsService(IMongoDatabaseSettings settings)
        {
            var team = new MongoClient(settings.ConnectionString);
            var database = team.GetDatabase(settings.DatabaseName);
            _teams = database.GetCollection<Team>(settings.CollectionName);
        }

        public async Task<IEnumerable<Team>> Get() =>
           await _teams.Find(team => true)
                       .SortBy(team => team.Name)
                       .ToListAsync<Team>();

        public async Task<Team> GetById(string id) =>
           await _teams.Find(team => team.Id == id)
                       .FirstOrDefaultAsync<Team>();

        public async Task<Team> GetByName(string name) =>
            await _teams.Find(team => team.Name.ToLower() == name.ToLower())
                        .FirstOrDefaultAsync<Team>();

        public async Task<Team> Create(Team teamIn)
        {
            foreach (var person in teamIn.People)
            {
                await PeopleAPIService.UpdateStatus(person.Id);
                person.IsAvailable = !person.IsAvailable;
            }

            await _teams.InsertOneAsync(teamIn);

            return teamIn;
        }

        public async Task<Team> Update(string id, Team teamIn)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            await _teams.ReplaceOneAsync<Team>(team => team.Id == id, teamIn);

            return team;
        }

        public async Task<Team> UpdateStatus(string id)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            team.IsAvailable = !team.IsAvailable;

            await _teams.ReplaceOneAsync<Team>(team => team.Id == id, team);

            return team;
        }

        public async Task<Team> Remove(string id)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            foreach (var person in team.People)
                await PeopleAPIService.UpdateStatus(person.Id);

            await _teams.DeleteOneAsync<Team>(team => team.Id == id);

            return team;
        }
    }
}
