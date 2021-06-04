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
    public class RiverRepository: IRiverRepository
    {
        public RiverRepository()
        {

        }
        public IEnumerable<River> GetAll()
        {
            GeoContext ctx = new GeoContext();

            return ctx.Rivers;
        }
        public River GetRiver(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsRiver(id))
            {
                List<RiverMapping> rms = new List<RiverMapping>();
                var r = ctx.Rivers
                    .Where(r => r.RiverId == id)
                    .Join(ctx.RiverMappings,
                    r => new { RiverId = r.RiverId },
                    rm => new { rm.RiverId },
                    (r, rm) => new
                    {
                        Name = r.Name,
                        RiverId = r.RiverId,
                        Length = r.Length,
                        CountryId = rm.CountryId
                    });

                foreach (var rm in r)
                {
                    RiverMapping rmData = new RiverMapping { RiverId = id, CountryId = rm.CountryId };
                    rms.Add(rmData);
                }

                if (rms.Count == 0)
                {
                    return ctx.Rivers.Where(r => r.RiverId == id).First();
                }

                River river = new River { RiverId = id, Name = r.First().Name, Length = r.First().Length, RiverMappings = rms };

                return river;
            }
            else throw new GeoException("River does not exist in db.");
        }
        public void AddRiver(River river, int id)
        {
            GeoContext ctx = new GeoContext();

            if (!ctx.Rivers.Any(r => r.Name == river.Name))
            {
                river.RiverMappings = new List<RiverMapping>
                { new RiverMapping { River = river, Country =  ctx.Countries.Single(c => c.CountryId == id)} };

                ctx.Rivers.Add(river);
                ctx.SaveChanges();
            }
            else throw new GeoException("River already added to db.");
        }
        public void RemoveRiver(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsRiver(id))
            {
                ctx.Rivers.Remove(ctx.Rivers.Single(r => r.RiverId == id));
                ctx.SaveChanges();
            }
            else throw new GeoException("River does not exist in db.");
        }
        public void UpdateRiver(River river)
        {
            GeoContext ctx = new GeoContext();

            if (ExistsRiver(river.RiverId))
            {
                ctx.Rivers.Update(river);
                ctx.SaveChanges();
            }
            else throw new GeoException("River does not exist in db.");
        }
        public bool ExistsRiver(int id)
        {
            GeoContext ctx = new GeoContext();

            if (ctx.Rivers.Any(r => r.RiverId == id))
            {
                return true;
            }
            else return false;
        }
    }
}
