using GeographyService.Models.Entities.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities
{
    public class River
    {
        public River()
        {

        }
        public River(string name, int length)
        {
            Name = name;
            Length = length;
        }
        [Key]
        public int RiverId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public ICollection<RiverMapping>? RiverMappings { get; set; }
    }
}
