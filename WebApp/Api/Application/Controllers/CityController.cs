using Api.Application.ViewModel;
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
        [ProducesResponseType(typeof(List<CityView>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<CityView>>> GetAll()
        {
            IEnumerable<CityDTO> cityDTOs = await _cityRepository.GetAll();

            if (cityDTOs == null || !cityDTOs.Any())
            {
                return NoContent();
            }

            List<CityView> cityViews = cityDTOs.Select(cityDTO => new CityView
            {
                CityName = cityDTO.CityName,
                StateName = cityDTO.StateName
            }).ToList();

            return Ok(cityViews);
        }

        [HttpGet("api/{id}")]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(404)]
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

        [HttpPost("api")]
        [ProducesResponseType(typeof(CityView), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CityView>> Create([FromBody] CityView cityView)
        {
            if (cityView == null || string.IsNullOrWhiteSpace(cityView.CityName) || string.IsNullOrWhiteSpace(cityView.StateName))
            {
                return BadRequest();
            }

            CityDTO cityDTO = new CityDTO
            {
                CityName = cityView.CityName,
                StateName = cityView.StateName
            };

            await _cityRepository.Create(cityDTO);

            return StatusCode(201, cityView);
        }

        [HttpPut("api/{id}")]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
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

            if (cityView == null || string.IsNullOrWhiteSpace(cityView.CityName) || string.IsNullOrWhiteSpace(cityView.StateName))
            {
                return BadRequest("Invalid input. CityName and StateName are required.");
            }

            CityView updatedCityView = new CityView
            {
                CityName = updatedCityDTO.CityName,
                StateName = updatedCityDTO.StateName
            };

            return Ok(updatedCityView);
        }

        [HttpDelete("api/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CityView>> Delete(int id)
        {
            bool deleted = await _cityRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
