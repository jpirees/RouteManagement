using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using People.API.Configuration;

namespace People.API.Services
{
    public class PeopleService
    {
        public IMongoCollection<Person> _people;

        public PeopleService(IMongoDatabaseSettings settings)
        {
            var person = new MongoClient(settings.ConnectionString);
            var database = person.GetDatabase(settings.DatabaseName);
            _people = database.GetCollection<Person>(settings.CollectionName);
        }

        public async Task<IEnumerable<Person>> Get() =>
            await _people.Find(person => true)
                         .SortBy(person => person.Name)
                         .ToListAsync<Person>();

        public async Task<Person> GetById(string id) =>
            await _people.Find(person => person.Id == id)
                         .FirstOrDefaultAsync<Person>();

        public async Task<IEnumerable<Person>> GetByName(string name) =>
            await _people.Find(person => person.Name.ToLower().Contains(name.ToLower()))
                         .SortBy(person => person.Name.ToLower())
                         .ToListAsync<Person>();

        public async Task<Person> Create(Person person)
        {
            await _people.InsertOneAsync(person);
            return person;
        }

        public async Task<Person> Update(string id, Person personIn)
        {
            var person = await GetById(id);

            if (person == null)
                return null;

            await _people.ReplaceOneAsync<Person>(person => person.Id == id, personIn);

            return personIn;
        }

        public async Task<Person> UpdateStatus(string id)
        {
            var person = await GetById(id);

            if (person == null)
                return null;

            person.IsAvailable = !person.IsAvailable;

            await _people.ReplaceOneAsync<Person>(person => person.Id == id, person);

            return person;
        }

        public async Task<Person> UpdateTeam(string id, string team)
        {
            var person = await GetById(id);

            if (person == null)
                return null;

            person.Team = team;

            await _people.ReplaceOneAsync<Person>(person => person.Id == id, person);

            return person;
        }

        public async Task<Person> Remove(string id)
        {
            var person = await GetById(id);

            if (person == null)
                return null;

            await _people.DeleteOneAsync<Person>(person => person.Id == id);

            return person;
        }
    }
}
