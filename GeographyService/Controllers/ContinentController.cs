using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Models.Interfaces;
using GeographyService.Models;
using GeographyService.Data;
using Microsoft.AspNetCore.JsonPatch;
using GeographyService.Models.Entities;
using GeographyService.Models.Repositories;
using GeographyService.Models.Entities.Mappings;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace GeographyService.Controllers
{
    [Route("api")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private IContinentRepository contRepos;
        private ILogger logger;

        public ContinentController(IContinentRepository contRepos, ILogger<ContinentController> logger)
        {
            this.contRepos = contRepos;
            this.logger = logger;
        }
        [HttpGet("Continent")]
        public IEnumerable<Continent> GetAll()
        {
            log("GetAll");
            return contRepos.GetAll();
        }
        [HttpGet("Continent/{id}")]
        public ActionResult<ContinentRestOutput> Get(int id)
        {
            log("Get");
            try
            {
                Continent continent = contRepos.GetContinent(id);
                List<string> countryUrls = new List<string>();
                ContinentRestOutput continentRestOutput = new ContinentRestOutput(id, continent.Name, continent.Population);

                if (continent.CountryMappings != null)
                {
                    foreach (CountryMapping cm in continent.CountryMappings)
                    {
                        string url = $"http://localhost:50055/api/continent/{id}/country/{cm.CountryId}";
                        countryUrls.Add(url);
                    }
                    continentRestOutput.CountryId = countryUrls;
                }
                return continentRestOutput;
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("Continent")]
        public ActionResult<Continent> Post([FromBody] Continent continent)
        {
            log("Post");
            try
            {
                contRepos.AddContinent(continent);
                return CreatedAtAction(nameof(Get), new { id = continent.ContinentId }, continent);  
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("Continent/{id}")]
        public ActionResult<Continent> Delete(int id)
        {
            log("Delete");
            ICountryRepository countRepos = new CountryRepository();

            if (!contRepos.ExistsContinent(id))
            {
                return NotFound();
            }
            if (countRepos.GetAll(id).Count() > 0)
            {
                GeoException ex = new GeoException("Continent still contains countries.");
                return BadRequest(ex.Message);
            }

            else try
            {
                contRepos.RemoveContinent(id);
                return NoContent();
            }
            catch (GeoException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Continent/{id}")]
        public ActionResult<Continent> Put(int id, [FromBody] Continent continent)
        {
            log("Put");
            if (continent == null || continent.ContinentId != id)
            {
                return BadRequest();
            }
            else if (!contRepos.ExistsContinent(id))
            {
                contRepos.AddContinent(continent);
                return CreatedAtAction(nameof(Get), new { id = continent.ContinentId }, continent);
            }
            contRepos.UpdateContinent(continent);
            return new NoContentResult();   
        }
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
