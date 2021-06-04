using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities.Mappings
{
    public partial class CountryMapping
    {
        public int ContinentId { get; set; }
        public int CountryId { get; set; }
        public Continent Continent { get; set; }
        public Country Country { get; set; }

    }
}
