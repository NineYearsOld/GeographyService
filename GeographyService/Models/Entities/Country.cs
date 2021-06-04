using GeographyService.Models.Entities.Mappings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities
{
    public partial class Country
    {
        public Country()
        {

        }
        public Country(string name, int surface)
        {
            Name = name;
            Surface = surface;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Surface { get; set; }
        [JsonIgnore]
        public virtual ICollection<CityMapping>? CityMappings { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryMapping>? CountryMappings { get; set; }
        public virtual ICollection<RiverMapping>? RiverMappings { get; set; }
    }
}
