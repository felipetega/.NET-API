using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CityController : Controller
    {
        // GET: CityController

        private static List<City> cities = new List<City>
        {
            new City("CityA","SP")
        };

        // GET LIST
        [HttpGet("api")]
        public ActionResult<List<City>> Index()
        {
            return cities;
        }

        // GET DETAIL
        [HttpGet]
        [Route("api/{id}")]
        public ActionResult<City> Details(int id)
        {
            if (id >= 0 && id < cities.Count)
            {
                return cities[id];
            }
            else
            {
                return NotFound();
            }
        }

        // CREATE
        [HttpPost("api/create")]
        public ActionResult<City> Create([FromBody] City newCity)
        {
            if (ModelState.IsValid)
            {
                int newId = cities.Count;
                newCity.Id = newId;

                cities.Add(newCity);

                return newCity;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // UPDATE
        [HttpPut("api/update/{id}")]
        public ActionResult<City> Edit(int id, [FromBody] City updatedCity)
        {
            if (id >= 0 && id < cities.Count)
            {
                City existingCity = cities[id];

                if (existingCity != null && ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(updatedCity.CityName))
                    {
                        existingCity.CityName = updatedCity.CityName;
                        existingCity.StateName = updatedCity.StateName;
                    }


                    return Ok(existingCity);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return NotFound();

            }
        }

        // PATCH
        [HttpPatch("api/patch/{id}")]
        public ActionResult<City> EditName(int id, [FromBody] City updatedCity)
        {
            if (id >= 0 && id < cities.Count)
            {
                City existingCity = cities[id];

                if (existingCity != null && ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(updatedCity.CityName))
                    {
                        existingCity.CityName = updatedCity.CityName;
                    }


                    return Ok(existingCity);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return NotFound();

            }
        }

        // DELETE
        [HttpDelete]
        [Route("api/delete/{id}")]
        public ActionResult<City> Delete(int id)
        {
            if (id >= 0 && id < cities.Count)
            {
                cities.RemoveAt(id);
                return Ok($"Cidade id:{id} foi removido com sucesso!");
            }
            else
            {
                return NotFound();
            }
        }


    }
}
