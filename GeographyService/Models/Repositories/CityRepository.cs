using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Data;
using GeographyService.Models.Interfaces;

namespace GeographyService.Models.Repositories
{
    public class CityRepository: ICityRepository
    {
        public CityRepository()
        {

        }
        public IEnumerable<City> GetAll()
        {
            using (GeoContext ctx = new GeoContext())
            {
                return ctx.Cities;
            }
        }
        public City GetCity(int id)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Cities.Any(i => i.Id == id))
                {
                    return (City)ctx.Cities.Where(i => i.Id == id);
                }
                else throw new GeoException("City does not exist in db.");
            }
        }
        public void AddCity(City city)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (!ctx.Cities.Any(n => n.Name == city.Name))
                {
                    ctx.Cities.Add(city);
                }
                else throw new GeoException("City already added to db.");
            }
        }
        public void RemoveCity(City city)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Cities.Any(n => n.Name == city.Name))
                {
                    ctx.Cities.Remove(city);
                }
                else throw new GeoException("City does not exist in db.");
            }
        }
        public void UpdateCity(City city)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Cities.Any(n => n.Name == city.Name))
                {
                    ctx.Cities.Update(city);
                }
                else throw new GeoException("City does not exist in db.");
            }
        }
    }
}
