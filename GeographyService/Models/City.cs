using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models
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
        public City(int id, string name, int population)
        {
            Id = id;
            Name = name;
            Population = population;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
    }
}
