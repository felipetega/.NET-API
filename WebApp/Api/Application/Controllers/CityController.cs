using Api.Application.ViewModel;
using Api.Infrastructure.Models;
using Api.Services.DTOs;
using Api.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("api")]
        public async Task<ActionResult<List<CityView>>> GetAll()
        {
            IEnumerable<CityDTO> cityDTOs = await _cityRepository.GetAll();
            List<CityView> cityViews = cityDTOs.Select(cityDTO => new CityView
            {
                CityName = cityDTO.CityName,
                StateName = cityDTO.StateName
            }).ToList();

            return Ok(cityViews);
        }


        [HttpGet("api/{id}")]
        public async Task<ActionResult<CityView>> GetById(int id)
        {
            CityDTO cityDTO = await _cityRepository.GetById(id);

            if (cityDTO == null)
            {
                return NotFound();
            }

            CityView cityView = new CityView
            {
                CityName = cityDTO.CityName,
                StateName = cityDTO.StateName
            };

            return Ok(cityView);
        }


        [HttpPost("api/create")]
        public async Task<ActionResult<CityView>> Create([FromBody] CityView cityView)
        {
            CityDTO cityDTO = new CityDTO
            {
                CityName = cityView.CityName,
                StateName = cityView.StateName
            };

            await _cityRepository.Create(cityDTO);


            return StatusCode(201, cityView);
        }


        [HttpPut("api/update/{id}")]
        public async Task<ActionResult<CityView>> Update([FromBody] CityView cityView, int id)
        {
            CityDTO cityDTO = new CityDTO
            {
                Id = id,
                CityName = cityView.CityName,
                StateName = cityView.StateName
            };

            CityDTO updatedCityDTO = await _cityRepository.Update(cityDTO, id);

            if (updatedCityDTO == null)
            {
                return NotFound();
            }

            CityView updatedCityView = new CityView
            {
                CityName = updatedCityDTO.CityName,
                StateName = updatedCityDTO.StateName
            };

            return Ok(updatedCityView);
        }



        [HttpDelete("api/delete/{id}")]
        public async Task<ActionResult<CityView>> Delete(int id)
        {
            bool deleted = await _cityRepository.Delete(id);
            return Ok(deleted);
        }

    }
}
