using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities.Mappings
{
    public class CityMapping
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public bool IsCapital { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }

    }
}
