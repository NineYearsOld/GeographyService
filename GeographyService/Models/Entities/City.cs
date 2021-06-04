using GeographyService.Models.Entities.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities
{
    public class City
    {
        public City()
        {

        }
        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int CityId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public ICollection<CityMapping>? CityMappings { get; set; }
    }
}
