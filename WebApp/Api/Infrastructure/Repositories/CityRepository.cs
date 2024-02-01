using Api.Infrastructure.Data;
using Api.Infrastructure.Models;
using Api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApiContext _context;
        public CityRepository(ApiContext apiContext) {
            _context = apiContext;
        }
        public async Task<List<City>> GetAll()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<City> GetById(int id)
        {
            return await _context.City.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<City> Create(City city)
        {
            await _context.City.AddAsync(city);
            await _context.SaveChangesAsync();
            return city;
        }
        public async Task<City> Update(City city, int id)
        {
            City cityById = await GetById(id);

            if (cityById==null)
            {
                throw new Exception("User not found");
            }
            cityById.CityName = city.CityName;
            cityById.StateName = city.StateName;

            _context.City.Update(cityById);
            await _context.SaveChangesAsync();

            return cityById;
        }

        public async Task<bool> Delete(int id)
        {
            City cityById = await GetById(id);

            if (cityById == null)
            {
                throw new Exception("User not found");
            }
            _context.City.Remove(cityById);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
