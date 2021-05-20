﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models
{
    public class GeographyException: Exception
    {
        public GeographyException(string message) : base(message)
        { 
        }
    }
}