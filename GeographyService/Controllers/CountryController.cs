using GeographyService.Models;
using GeographyService.Models.Entities;
using GeographyService.Models.Interfaces;
using GeographyService.Models.Repositories;
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
    [Route("api/Continent")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countRepos;
        private readonly ILogger logger;

        public CountryController(ICountryRepository countRepos, ILogger<CountryController> logger)
        {
            this.countRepos = countRepos;
            this.logger = logger;
        }
        [HttpGet("{continentId}/Country")]
        public ActionResult<IEnumerable<Country>> GetAll(int continentId)
        {
            log("Getall");
            try
            {
                List<Country> countries = (List<Country>)countRepos.GetAll(continentId);
                return CreatedAtAction(nameof(GetAll), new { id = continentId }, countries);
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{continentId}/Country/{countryId}")]
        public ActionResult<Country> GetCountry(int continentId, int countryId)
        {
            log("GetCountry");
            try
            {
                return countRepos.GetCountry(continentId, countryId);
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("{id}/Country")]
        public ActionResult<Country> Post(int id, [FromBody]Country country)
        {
            log("Post");
            try
            {
                countRepos.AddCountry(country, id);
                return CreatedAtAction(nameof(GetCountry), new { continentId = id, countryId = country.CountryId }, country);
            }
            catch (GeoException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{continentId}/Country/{countryId}")]
        public ActionResult<Continent> Delete(int continentId, int countryId)
        {
            log("Delete");
            ICityRepository cityRepos = new CityRepository();

            if (cityRepos.GetAll(continentId, countryId).Count() > 0)
            {
                GeoException ex = new GeoException("Country still contains cities.");
                return BadRequest(ex.Message);
            }

            else try
            {
                countRepos.RemoveCountry(continentId, countryId);
                return NoContent();
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{continentId}/Country/{countryId}")]
        public ActionResult<Continent> Put(int continentId, int countryId, [FromBody] Country country)
        {
            log("Put");

            if (country == null || country.CountryId != countryId)
            {
                return BadRequest();
            }
            else if (!countRepos.ExistsCountry(countryId))
            {
                countRepos.AddCountry(country, continentId);
                return CreatedAtAction(nameof(GetCountry), new { continentId = continentId, countryId = country.CountryId }, country);
            }
            countRepos.UpdateCountry(country);
            return new NoContentResult();
        }
        [HttpPatch("{continentId}/Country/{countryId}")]
        public ActionResult<Continent> Patch(int id, [FromBody] JsonPatchDocument continentPatch)
        {
            log("Patch");
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
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
