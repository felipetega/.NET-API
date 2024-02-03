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
        // SWAGGER DOCUMENTATION
        [ProducesResponseType(typeof(List<CityView>), 200)] // OK
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<ActionResult<List<CityView>>> GetAll()
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpGet("api/{id}")]
        // SWAGGER DOCUMENTATION
        [ProducesResponseType(typeof(CityView), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<ActionResult<CityView>> GetById(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpPost("api/create")]
        // SWAGGER DOCUMENTATION
        [ProducesResponseType(typeof(CityView), 201)] // Created
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<ActionResult<CityView>> Create([FromBody] CityView cityView)
        {
            try
            {
                if (cityView == null || string.IsNullOrWhiteSpace(cityView.CityName) || string.IsNullOrWhiteSpace(cityView.StateName))
                {
                    return BadRequest("Invalid input. CityName and StateName are required.");
                }

                CityDTO cityDTO = new CityDTO
                {
                    CityName = cityView.CityName,
                    StateName = cityView.StateName
                };

                await _cityRepository.Create(cityDTO);

                return StatusCode(201, cityView);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }




        [HttpPut("api/update/{id}")]
        // SWAGGER DOCUMENTATION
        [ProducesResponseType(typeof(CityView), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<ActionResult<CityView>> Update([FromBody] CityView cityView, int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }





        [HttpDelete("api/delete/{id}")]
        // SWAGGER DOCUMENTATION
        [ProducesResponseType(typeof(bool), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(500)] // Internal Server Error
        public async Task<ActionResult<CityView>> Delete(int id)
        {
            try
            {
                bool deleted = await _cityRepository.Delete(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}
