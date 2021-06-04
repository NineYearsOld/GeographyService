using GeographyService.Data;
using GeographyService.Models;
using GeographyService.Models.Entities;
using GeographyService.Models.Entities.Mappings;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RiverController : ControllerBase
    {
        private readonly IRiverRepository riverRepos;
        private readonly ILogger logger;
        public RiverController(IRiverRepository riverRepos, ILogger<RiverController> logger)
        {
            this.riverRepos = riverRepos;
            this.logger = logger;
        }
        [HttpGet]
        public IEnumerable<River> GetAll()
        {
            log("GetAll");
            return riverRepos.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<RiverRestOutput> Get(int id)
        {
            log("Get");
            try
            {
                River river = riverRepos.GetRiver(id);
                List<string> countryUrls = new List<string>();
                RiverRestOutput riverRestOutput = new RiverRestOutput(id, river.Name, river.Length);

                if (river.RiverMappings != null)
                {
                    foreach (RiverMapping rm in river.RiverMappings)
                    {
                        string url = $"http://localhost:50055/api/continent/{id}/country/{rm.CountryId}";
                        countryUrls.Add(url);
                    }
                    riverRestOutput.CountryId = countryUrls;
                }
                return riverRestOutput;
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("{countryId}")]
        public ActionResult<River> Post([FromBody] River river, int countryId)
        {
            log("Post");
            try
            {
                riverRepos.AddRiver(river, countryId);
                return CreatedAtAction(nameof(Get), new { id = river.RiverId }, river);
            }
            catch (GeoException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<River> Delete(int id)
        {
            log("Delete");
            if (!riverRepos.ExistsRiver(id))
            {
                return NotFound();
            }

            else try
            {
                riverRepos.RemoveRiver(id);
                return NoContent();
            }
            catch (GeoException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<River> Put(int id, [FromBody] River river, int countryId)
        {
            log("Put");
            if (river == null || river.RiverId != id)
            {
                return BadRequest();
            }
            else if (!riverRepos.ExistsRiver(id))
            {
                riverRepos.AddRiver(river, countryId);
                return CreatedAtAction(nameof(Get), new { id = river.RiverId }, river);
            }
            riverRepos.UpdateRiver(river);
            return new NoContentResult();
        }
        /*
        [HttpPatch("{id}")]
        public ActionResult<River> Patch(int id, [FromBody] JsonPatchDocument riverPatch)
        {            
            log("Patch");
            try
            {
                River continentRO = riverRepos.GetRiver(id);
                River river = new River { Name = continentRO.Name, ContinentId = continentRO.ContinentId, Population = continentRO.Population };
                continentPatch.ApplyTo(continent);
                contRepos.UpdateContinent(continent);
                return continent;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }*/
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
