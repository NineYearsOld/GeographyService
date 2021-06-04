using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Entities.Mappings
{
    public class RiverMapping
    {
        public int CountryId { get; set; }
        public int RiverId { get; set; }
        public Country Country { get; set; }
        public River River { get; set; }
    }
}
