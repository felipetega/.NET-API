using Api.Infrastructure.Data;
using Api.Infrastructure.Models;
using Api.Services.DTOs;
using Api.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApiContext _context;
        public CityRepository(ApiContext apiContext)
        {
            _context = apiContext;
        }
        public async Task<List<CityDTO>> GetAll()
        {
            return await _context.City.Select(s => new CityDTO() {
                Id = s.Id, CityName = s.CityName, StateName = s.StateName 
            }).ToListAsync();

        }

        public async Task<CityDTO> GetById(int id)
        {
            return await _context.City
                .Select(s => new CityDTO() {
                    Id = s.Id,
                    CityName = s.CityName,
                    StateName = s.StateName
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<CityDTO> Create(CityDTO cityDTO)
        {
            City city = new City(cityDTO.CityName, cityDTO.StateName);


            await _context.City.AddAsync(city);
            await _context.SaveChangesAsync();


            return cityDTO;
        }
        public async Task<CityDTO> Update(CityDTO city, int id)
        {
            City cityById = await FindId(id);

            if (cityById == null)
            {
                throw new Exception("City not found");
            }

            cityById.CityName = city.CityName;
            cityById.StateName = city.StateName;

            _context.City.Update(cityById);
            await _context.SaveChangesAsync();

            var updatedCityDTO = new CityDTO
            {
                Id = cityById.Id,
                CityName = cityById.CityName,
                StateName = cityById.StateName
            };

            return updatedCityDTO;
        }

        public async Task<bool> Delete(int id)
        {
            City cityById = await FindId(id);

            if (cityById == null)
            {
                throw new Exception("City not found");
            }

            _context.City.Remove(cityById);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<City> FindId(int id)
        {
            return await _context.City.FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
