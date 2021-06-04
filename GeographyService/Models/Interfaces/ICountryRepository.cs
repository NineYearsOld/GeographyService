using GeographyService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country, int id);
        Country GetCountry(int continentId, int countryid);
        IEnumerable<Country> GetAll(int continentId);
        void RemoveCountry(int continentId, int countryId);
        void UpdateCountry(Country country);
        bool ExistsCountry(int id);
    }
}
