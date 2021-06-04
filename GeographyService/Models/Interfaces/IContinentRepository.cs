using GeographyService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface IContinentRepository
    {
        void AddContinent(Continent continent);
        Continent GetContinent(int id);
        IEnumerable<Continent> GetAll();
        void RemoveContinent(int id);
        void UpdateContinent(Continent continent);
        bool ExistsContinent(int id); 
    }
}
