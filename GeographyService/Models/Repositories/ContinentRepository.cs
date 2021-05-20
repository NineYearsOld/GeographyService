using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Data;
using GeographyService.Models.Interfaces;

namespace GeographyService.Models.Repositories
{
    public class ContinentRepository: IContinentRepository
    {
        public ContinentRepository()
        {
            using (GeoContext ctx = new GeoContext())
            {
                Continent ct = new Continent("Europa", 700);
                ctx.Continents.Add(ct);
            }
        }
        public IEnumerable<Continent> GetAll()
        {
            using (GeoContext ctx = new GeoContext())
            {
                return ctx.Continents;
            }
        }
        public Continent GetContinent(int id)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Continents.Any(i => i.Id == id))
                {
                    return (Continent)ctx.Continents.Where(i => i.Id == id);
                }
                else throw new GeoException("Continent does not exist in db.");
            }
        }
        public void AddContinent(Continent continent)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (!ctx.Continents.Any(n => n.Name == continent.Name))
                {
                    ctx.Continents.Add(continent);
                }
                else throw new GeoException("Continent already added to db.");
            }
        }
        public void RemoveContinent(Continent continent)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Continents.Any(n => n.Name == continent.Name))
                {
                    ctx.Continents.Remove(continent);
                }
                else throw new GeoException("Continent does not exist in db.");
            }
        }
        public void UpdateContinent(Continent continent)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Continents.Any(n => n.Name == continent.Name))
                {
                    ctx.Continents.Update(continent);
                }
                else throw new GeoException("Continent does not exist in db.");
            }
        }
    }
}
