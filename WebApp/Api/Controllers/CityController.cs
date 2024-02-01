using Api.Infrastructure.Models;
using Api.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("api")]
        public async Task<ActionResult<List<City>>> GetAll()
        {
            List<City> cities = await _cityRepository.GetAll();
            return Ok(cities);
        }

        [HttpGet("api/{id}")]
        public async Task<ActionResult<City>> GetById(int id)
        {
            City city = await _cityRepository.GetById(id);
            return Ok(city);
        }

        [HttpPost("api/create")]
        public async Task<ActionResult<City>> Create([FromBody] City cityModel)
        {
            City city = await _cityRepository.Create(cityModel);
            return Ok(city);
        }

        [HttpPut("api/update/{id}")]
        public async Task<ActionResult<City>> Update([FromBody] City cityModel, int id)
        {
            cityModel.Id = id;
            City city = await _cityRepository.Update(cityModel, id);
            return Ok(city);
        }

        [HttpDelete("api/delete/{id}")]
        public async Task<ActionResult<City>> Delete(int id)
        {
            bool deleted = await _cityRepository.Delete(id);
            return Ok(deleted);
        }

    }
}
