using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Data;
using GeographyService.Models.Entities;
using GeographyService.Models.Entities.Mappings;
using GeographyService.Models.Interfaces;

namespace GeographyService.Models.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        public CountryRepository()
        {

        }
        public IEnumerable<Country> GetAll(int continentId)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Continents.Any(c => c.ContinentId == continentId))
            {

                List<Country> cts = new List<Country>();

                var countries  = ctx.Countries
                    .Join(ctx.CountryMappings,
                    c => new { c.CountryId },
                    cm => new { cm.CountryId },
                    (c, cm)
                    => new
                    {
                        Name = c.Name,
                        CountryId = c.CountryId,
                        Surface = c.Surface,
                        ContinentId = cm.ContinentId
                    }).Where(c => c.ContinentId == continentId);

                foreach (var ct in countries)
                {
                    Country country = new Country(ct.Name, ct.Surface);
                    country.CountryId = ct.CountryId;
                    cts.Add(country);
                }
                return cts;

            }

            else throw new GeoException("Continent does not exist in db.");            
        }
        public Country GetCountry(int continentId, int countryId)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Countries.Any(i => i.CountryId == countryId))
            {
                var c = ctx.Countries
                    .Join(ctx.CountryMappings,
                    c => new { c.CountryId },
                    cm => new { cm.CountryId },
                    (c, cm) 
                    => new
                    {
                        Name = c.Name,
                        CountryId = c.CountryId,
                        Surface = c.Surface,
                        ContinentId = cm.ContinentId
                    }).
                    Where(c => c.CountryId == countryId);

                if (c.First().ContinentId != continentId)
                {
                    throw new GeoException("Continent id does not match that which the queried country is located in.");
                }

                Country country = new Country(c.First().Name, c.First().Surface);
                country.CountryId = countryId;

                return country;
            }
            else throw new GeoException("Country does not exist in db.");
        }
        public void AddCountry(Country country, int id)
        {
            GeoContext ctx = new GeoContext();

            if (!ctx.Countries.Any(c => c.Name == country.Name))
            {
                country.CountryMappings = new List<CountryMapping>
                { new CountryMapping { Country = country, Continent = ctx.Continents.Single(c => c.ContinentId == id) } };

                ctx.Countries.Add(country);
                ctx.SaveChanges();
            }
            else throw new GeoException("Country already added to db.");
        }
        public void RemoveCountry(int continentId, int countryId)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsCountry(countryId))
            {
                ctx.Countries.Remove(ctx.Countries.Single(c => c.CountryId == countryId));
                ctx.SaveChanges();
            }
            else throw new GeoException("Country does not exist in db.");
        }
        public void UpdateCountry(Country country)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsCountry(country.CountryId))
            {
                ctx.Countries.Update(country);
                ctx.SaveChanges();
            }
            else throw new GeoException("Country does not exist in db.");
        }
        public bool ExistsCountry(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Countries.Any(c => c.CountryId == id))
            {
                return true;
            }
            else return false;
        }
    }
}
