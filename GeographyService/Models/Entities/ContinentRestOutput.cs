using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities
{
    public class ContinentRestOutput
    {
        public ContinentRestOutput(int continentId, string name, int population)
        {
            ContinentId = continentId;
            Name = name;
            Population = population;
        }
        public int ContinentId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public List<string> CountryId { get; set; }
    }
}
