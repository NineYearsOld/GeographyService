using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities
{
    public class RiverRestOutput
    {
        public RiverRestOutput(int riverId, string name, int length)
        {
            RiverId = riverId;
            Name = name;
            Length = length;
        }
        public int RiverId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public List<string> CountryId { get; set; }
    }
}
