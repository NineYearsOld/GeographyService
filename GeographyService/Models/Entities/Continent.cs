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
    public partial class Continent
    {
        public Continent()
        {

        }
        public Continent(string name)
        {
            Name = name;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int ContinentId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public ICollection<CountryMapping>? CountryMappings { get; set; }
    }
}
