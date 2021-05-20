using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Data;
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
            using (GeoContext ctx = new GeoContext())
            {
                return ctx.Rivers;
            }
        }
        public River GetRiver(int id)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Rivers.Any(i => i.Id == id))
                {
                    return (River)ctx.Rivers.Where(i => i.Id == id);
                }
                else throw new GeoException("River does not exist in db.");
            }
        }
        public void AddRiver(River river)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (!ctx.Rivers.Any(n => n.Name == river.Name))
                {
                    ctx.Rivers.Add(river);
                }
                else throw new GeoException("River already added to db.");
            }
        }
        public void RemoveRiver(River river)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Rivers.Any(n => n.Name == river.Name))
                {
                    ctx.Rivers.Remove(river);
                }
                else throw new GeoException("River does not exist in db.");
            }
        }
        public void UpdateRiver(River river)
        {
            using (GeoContext ctx = new GeoContext())
            {
                if (ctx.Rivers.Any(n => n.Name == river.Name))
                {
                    ctx.Rivers.Update(river);
                }
                else throw new GeoException("River does not exist in db.");
            }
        }
    }
}
