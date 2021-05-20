using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Models.Interfaces;
using GeographyService.Models;
using GeographyService.Data;

namespace GeographyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeographyController : ControllerBase
    {
        private IContinentRepository contRepos;
        private IRiverRepository riverRepository;
        private ICountryRepository countRepos;
        private ICityRepository cityRepository;

        public GeographyController(IContinentRepository contRepos, IRiverRepository riverRepository, ICountryRepository countRepos, ICityRepository cityRepository)
        {
            this.contRepos = contRepos;
            this.riverRepository = riverRepository;
            this.countRepos = countRepos;
            this.cityRepository = cityRepository;
        }
        public IEnumerable<Continent> GetAll()
        {
            return contRepos.GetAll();
        }

    }
}
