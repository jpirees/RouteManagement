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

        public async Task<List<Team>> GetTeamsByCity(string cityId) =>
            await _teams.Find(team => team.OperatingCity.Id == cityId)
                        .SortBy(team => team.Name)
                        .ToListAsync<Team>();

        public async Task<Team> Create(Team teamIn)
        {
            foreach (var person in teamIn.People)
            {
                await PeopleAPIService.UpdateStatus(person.Id);
                await PeopleAPIService.UpdateTeam(person.Id, teamIn.Name);

                person.IsAvailable = !person.IsAvailable;
                person.Team = teamIn.Name;
            }

            await _teams.InsertOneAsync(teamIn);

            return teamIn;
        }

        public async Task<Team> Update(string id, Team teamIn)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            if (team.Name != teamIn.Name)
                foreach (var person in team.People)
                    await PeopleAPIService.UpdateTeam(person.Id, teamIn.Name);

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

        public async Task<Team> UpdateToInsert(string id, Person personIn)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            personIn.IsAvailable = false;
            personIn.Team = team.Name;

            var filter = Builders<Team>.Filter.Where(team => team.Id == id);
            var update = Builders<Team>.Update.Push("People", personIn);

            await PeopleAPIService.UpdateStatus(personIn.Id);
            await PeopleAPIService.UpdateTeam(personIn.Id, team.Name);

            await _teams.UpdateOneAsync(filter, update);

            return team;
        }

        public async Task<Team> UpdateToRemove(string id, Person personOut)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            var filter = Builders<Team>.Filter.Where(team => team.Id == id);
            var update = Builders<Team>.Update.Pull("People", personOut);

            await PeopleAPIService.UpdateStatus(personOut.Id);
            await PeopleAPIService.UpdateTeam(personOut.Id, null);

            await _teams.UpdateOneAsync(filter, update);

            return team;
        }

        public async Task<Team> Remove(string id)
        {
            var team = await GetById(id);

            if (team == null)
                return null;

            foreach (var person in team.People)
            {
                await PeopleAPIService.UpdateStatus(person.Id);
                await PeopleAPIService.UpdateTeam(person.Id, null);
            }

            await _teams.DeleteOneAsync<Team>(team => team.Id == id);

            return team;
        }
    }
}
