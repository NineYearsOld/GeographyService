using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models
{
    public class Country
    {
        public Country()
        {

        }
        public Country(string name, int population, int surface)
        {
            Name = name;
            Population = population;
            Surface = surface;
        }
        public Country(int id, string name, int population, int surface)
        {
            Id = id;
            Name = name;
            Population = population;
            Surface = surface;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Surface { get; set; }
    }
}
