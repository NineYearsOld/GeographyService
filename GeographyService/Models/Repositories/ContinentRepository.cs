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
    public class ContinentRepository: IContinentRepository
    {
        public ContinentRepository()
        {

        }
        public IEnumerable<Continent> GetAll()
        {
            GeoContext ctx = new GeoContext();

            return ctx.Continents;
        }
        public Continent GetContinent(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsContinent(id))
            {
                List<CountryMapping> cms = new List<CountryMapping>();
                var c = ctx.Continents
                    .Where(c => c.ContinentId == id)
                    .Join(ctx.CountryMappings,
                    c => new { c.ContinentId },
                    cm => new { cm.ContinentId },
                    (c, cm) => new
                    {
                        Name = c.Name,
                        ContinentId = c.ContinentId,
                        Population = c.Population,
                        CountryId = cm.CountryId
                    });

                foreach (var cm in c)
                {
                    CountryMapping cmData = new CountryMapping { ContinentId = id, CountryId = cm.CountryId };
                    cms.Add(cmData);
                }

                if (cms.Count == 0)
                {
                    return ctx.Continents.Where(c => c.ContinentId == id).First();
                }

                Continent continent = new Continent { ContinentId = id, Name = c.First().Name, Population = c.First().Population, CountryMappings = cms };

                return continent;
            }
            else throw new GeoException("Continent does not exist in db.");
        }
        public void AddContinent(Continent continent)
        {
            GeoContext ctx = new GeoContext();

            if (!ctx.Continents.Any(c => c.Name == continent.Name))
            {
                ctx.Continents.Add(continent);
                ctx.SaveChanges();
            }
            else throw new GeoException("Continent already added to db.");
        }
        public void RemoveContinent(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsContinent(id))
            {
                ctx.Continents.Remove(ctx.Continents.Single(c => c.ContinentId == id));
                ctx.SaveChanges();
            }
            else throw new GeoException("Continent does not exist in db.");
        }
        public void UpdateContinent(Continent continent)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsContinent(continent.ContinentId))
            {
                ctx.Continents.Update(continent);
                ctx.SaveChanges();
            }
            else throw new GeoException("Continent does not exist in db.");
        }
        public bool ExistsContinent(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Continents.Any(c => c.ContinentId == id))
            {
                return true;
            }
            else return false;
        }
    }  
}
