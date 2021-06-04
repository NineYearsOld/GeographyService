using GeographyService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface ICityRepository
    {
        void AddCity(int continentId, int countryId, City city, int? capital);
        City GetCity(int id, int countryId, int continentId);
        IEnumerable<City> GetAll(int continentId, int countryId);
        void RemoveCity(int continentId, int countryId, int cityId);
        void UpdateCity(int continentId, int countryId, City city);
        bool ExistsCity(int id);
    }
}
