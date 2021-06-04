using GeographyService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface IRiverRepository
    {
        void AddRiver(River river, int countryId);
        River GetRiver(int id);
        IEnumerable<River> GetAll();
        void RemoveRiver(int riverId);
        void UpdateRiver(River river);
        bool ExistsRiver(int id);
    }
}
