using System.Collections.Generic;
using System.Threading.Tasks;
using Cities.API.Configuration;
using Models;
using MongoDB.Driver;

namespace Cities.API.Services
{
    public class CitiesService
    {
        public IMongoCollection<City> _cities;

        public CitiesService(IMongoDatabaseSettings settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _cities = database.GetCollection<City>(settings.CollectionName);
        }

        public async Task<IEnumerable<City>> Get() =>
            await _cities.Find(city => true)
                         .SortBy(city => city.Name)
                         .ToListAsync<City>();

        public async Task<City> GetById(string id) =>
            await _cities.Find(city => city.Id == id)
                         .FirstOrDefaultAsync<City>();

        public async Task<City> GetByName(string name) =>
            await _cities.Find(city => city.Name.ToLower() == name.ToLower())
                         .FirstOrDefaultAsync<City>();

        public async Task<IEnumerable<City>> GetCitiesByState(string state) =>
           await _cities.Find(city => city.State.ToLower() == state.ToLower())
                        .SortBy(city => city.Name)
                        .ToListAsync<City>();

        public async Task<City> Create(City city)
        {
            await _cities.InsertOneAsync(city);
            return city;
        }

        public async Task<City> Update(string id, City cityIn)
        {
            var city = await GetById(id);

            if (city == null)
                return null;

            await _cities.ReplaceOneAsync<City>(city => city.Id == id, cityIn);

            return cityIn;
        }

        public async Task<City> Remove(string id)
        {
            var city = await GetById(id);

            if (city == null)
                return null;

            await _cities.DeleteOneAsync<City>(city => city.Id == id);

            return city;
        }
    }
}
