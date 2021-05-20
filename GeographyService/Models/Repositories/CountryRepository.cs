using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Data;
using GeographyService.Models.Interfaces;

namespace GeographyService.Models.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        public CountryRepository()
        {

        }
        public IEnumerable<Country> GetAll()
        {
            using (GeoContext ctx = new GeoContext())
            {
                return ctx.Countries;
            }
        }
        public Country GetCountry(int id)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Countries.Any(i => i.Id == id))
                {
                    return (Country)ctx.Countries.Where(i => i.Id == id);
                }
                else throw new GeoException("Country does not exist in db.");
            }
        }
        public void AddCountry(Country country)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (!ctx.Countries.Any(n => n.Name == country.Name))
                {
                    ctx.Countries.Add(country);
                }
                else throw new GeoException("Country already added to db.");
            }
        }
        public void RemoveCountry(Country country)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Countries.Any(n => n.Name == country.Name))
                {
                    ctx.Countries.Remove(country);
                }
                else throw new GeoException("Country does not exist in db.");
            }
        }
        public void UpdateCountry(Country country)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Countries.Any(n => n.Name == country.Name))
                {
                    ctx.Countries.Update(country);
                }
                else throw new GeoException("Country does not exist in db.");
            }
        }
    }
}
