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
    public class CityRepository: ICityRepository
    {
        public CityRepository()
        {

        }
        public IEnumerable<City> GetAll(int continentId, int countryId)
        {
            GeoContext ctx = new GeoContext();

            var citiesData = ctx.Cities.Join(ctx.CityMappings,
                c => new { c.CityId },
                cm => new { cm.CityId },
                (c, cm) => 
                new
                {
                    CityId = c.CityId,
                    Name = c.Name,
                    Population = c.Population,
                    IsCapital = cm.IsCapital,
                    CountryId = cm.CountryId
                }).Join(ctx.CountryMappings,
                c => new { c.CountryId },
                cm => new { cm.CountryId },
                (c, cm) =>
                new
                {
                    CityId = c.CityId,
                    Name = c.Name,
                    Population = c.Population,
                    IsCapital = c.IsCapital,
                    CountryId = c.CountryId,
                    ContinentId = cm.ContinentId
                }).Where(c => c.ContinentId == continentId);

            List<City> cities = new List<City>();

            foreach (var city in citiesData)
            {
                City c = new City { Name = city.Name, CityId = city.CityId, Population = city.Population };
                cities.Add(c);
            }

            return cities;
        }
        public City GetCity(int cityId, int countryId, int continentId)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsCity(cityId))
            {
                var city = ctx.Cities
                .Join(ctx.CityMappings,
                c => new { c.CityId },
                cm => new { cm.CityId },
                (c, cm) =>
                new
                {
                    CityId = c.CityId,
                    CountryId = cm.CountryId,
                    Name = c.Name,
                    Population = c.Population
                })
                .Join(ctx.CountryMappings,
                c => new { c.CountryId },
                cm => new { cm.CountryId },
                (c, cm) =>
                new
                {
                    CityId = c.CityId,
                    CountryId = c.CountryId,
                    ContinentId = cm.ContinentId,
                    Name = c.Name,
                    Population = c.Population
                }).Where(c => c.CityId == cityId);

                if (cityId != city.First().CityId || countryId != city.First().CountryId || continentId != city.First().ContinentId)
                {
                    throw new GeoException("Country or continent id do not match that which the city is located at.");
                }

                City c = new City { CityId = city.First().CityId, Name = city.First().Name, Population = city.First().Population };

                return c;
            }

            else throw new GeoException("City does not exist in db.");
        }
        public void AddCity(int continentId, int countryId, City city, int? capital)
        {
            GeoContext ctx = new GeoContext();
            if (!ctx.Cities.Any(n => n.Name == city.Name))
            {
                Country country = ctx.Countries.Where(c => c.CountryId == countryId).First();
                Continent continent = ctx.Continents.Where(c => c.ContinentId == continentId).First();

                if (capital == 1)
                {
                    city.CityMappings = new List<CityMapping>
                    { new CityMapping { City = city, IsCapital = true, Country = ctx.Countries.Single(c => c.CountryId == countryId) } };
                }
                else
                {
                    city.CityMappings = new List<CityMapping>
                    { new CityMapping { City = city, Country = ctx.Countries.Single(c => c.CountryId == countryId) } };
                }

                country.Population += city.Population;
                continent.Population += city.Population;

                ctx.Countries.Update(country);
                ctx.Continents.Update(continent);
                ctx.Cities.Add(city);
                ctx.SaveChanges();
            }
            else throw new GeoException("City already added to db.");
        }
        public void RemoveCity(int continentId, int countryId, int cityId)
        {
            GeoContext ctx = new GeoContext();
            if (ctx.Cities.Any(c => c.CityId == cityId))
            {
                City city = ctx.Cities.Where(c => c.CityId == cityId).First();
                Country country = ctx.Countries.Where(c => c.CountryId == countryId).First();
                Continent continent = ctx.Continents.Where(c => c.ContinentId == continentId).First();

                country.Population -= city.Population;
                continent.Population -= city.Population;

                ctx.Continents.Update(continent);
                ctx.Countries.Update(country);
                ctx.Cities.Remove(city);
                ctx.SaveChanges();
            }
            else throw new GeoException("City does not exist in db.");
        }
        public void UpdateCity(int continentId, int countryId, City city)
        {
            GeoContext ctx = new GeoContext();
            if (ctx.Cities.Any(n => n.Name == city.Name))
            {
                GeoContext popContext = new GeoContext();
                int oldCityPopulation = popContext.Cities.Where(c => c.CityId == city.CityId).First().Population;
                Continent continent = ctx.Continents.Where(c => c.ContinentId == continentId).First();
                Country country = ctx.Countries.Where(c => c.CountryId == countryId).First();

                if (oldCityPopulation > city.Population)
                {
                    continent.Population -= city.Population;
                    country.Population -= city.Population;
                }

                else
                {
                    continent.Population += city.Population;
                    country.Population += city.Population;
                }

                ctx.Continents.Update(continent);
                ctx.Countries.Update(country);
                ctx.Cities.Update(city);
                ctx.SaveChanges();
            }
            else throw new GeoException("City does not exist in db.");
        }

        public bool ExistsCity(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Cities.Any(c => c.CityId == id))
            {
                return true;
            }
            else return false;
        }
    }
}
