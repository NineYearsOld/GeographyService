using GeographyService.Models;
using GeographyService.Models.Entities;
using GeographyService.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Controllers
{
    [Route("api")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepos;
        private readonly ILogger logger;

        public CityController(ICityRepository cityRepos, ILogger<CityController> logger)
        {
            this.cityRepos = cityRepos;
            this.logger = logger;
        }
        [HttpGet("Continent/{continentId}/Country/{countryId}/City")]
        public IEnumerable<City> GetAll(int continentId, int countryId)
        {
            log("GetAll");
            return cityRepos.GetAll(continentId, countryId);
        }
        [HttpGet("Continent/{continentId}/Country/{countryId}/City/{cityId}")]
        public ActionResult<City> GetCity(int continentId, int countryId, int cityId)
        {
            log("GetCity");
            try
            {
                return cityRepos.GetCity(cityId, countryId, continentId);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPost, Route("Continent/{continentId}/Country/{countryId}/City/"),
                   Route("Continent/{continentId}/Country/{countryId}/City/{capital}")]
        public ActionResult<Continent> Post(int continentId, int countryId, [FromBody] City city, int capital)
        {
            log("Post");
            try
            {
                cityRepos.AddCity(continentId, countryId, city, capital);
                return CreatedAtAction(nameof(GetCity), new { continentId = continentId, countryId = countryId, cityId = city.CityId }, city);
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Continent/{continentId}/Country/{countryId}/City/{cityId}")]
        public ActionResult<Continent> Delete(int continentId, int countryId, int cityId)
        {
            log("Delete");
            try
            {
                cityRepos.RemoveCity(continentId, countryId, cityId);
                return NoContent();
            }
            catch (GeoException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut, Route("Continent/{continentId}/Country/{countryId}/City/{cityId}"),
                  Route("Continent/{continentId}/Country/{countryId}/City/{cityId}/{capital}")]
        public ActionResult<City> Put(int continentId, int countryId, [FromBody] City city, int cityId, int capital)
        {
            log("Put");
            if (city == null || city.CityId != cityId)
            {
                return BadRequest();
            }
            else if (!cityRepos.ExistsCity(cityId))
            {
                cityRepos.AddCity(continentId, countryId, city, capital);
                return CreatedAtAction(nameof(GetCity), new { continentId = continentId, countryId = countryId, cityId = city.CityId }, city);
            }
            cityRepos.UpdateCity(continentId, countryId, city);
            return new NoContentResult();
        }/*
        [HttpPatch("Continent/{id}")]
        public ActionResult<Continent> Patch(int id, [FromBody] JsonPatchDocument continentPatch)
        {
            log("Patch");
            try
            {
                Continent continentRO = contRepos.GetContinent(id);
                Continent continent = new Continent { Name = continentRO.Name, ContinentId = continentRO.ContinentId, Population = continentRO.Population };
                continentPatch.ApplyTo(continent);
                contRepos.UpdateContinent(continent);
                return continent;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        */
        public string getDate()
        {
            var culture = new CultureInfo("nl-BE");
            string dateTime = DateTime.Now.ToString(culture);
            return dateTime;
        }
        public void log(string request)
        {
            logger.LogInformation($"{request} called at: {getDate()}");
        }
    }
}
