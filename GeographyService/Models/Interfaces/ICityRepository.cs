using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface ICityRepository
    {
        void AddCity(City city);
        City GetCity(int id);
        IEnumerable<City> GetAll();
        void RemoveCity(City city);
        void UpdateCity(City city);
    }
}
