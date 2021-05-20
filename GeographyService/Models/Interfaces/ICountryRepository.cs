using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);
        Country GetCountry(int id);
        IEnumerable<Country> GetAll();
        void RemoveCountry(Country country);
        void UpdateCountry(Country country);
    }
}
