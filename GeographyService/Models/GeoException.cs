using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models
{
    public class GeoException: Exception
    {
        public GeoException(string message) : base(message)
        { 
        }
    }
}
