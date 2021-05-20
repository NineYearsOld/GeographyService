using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models.Interfaces
{
    public interface IRiverRepository
    {
        void AddRiver(River river);
        River GetRiver(int id);
        IEnumerable<River> GetAll();
        void RemoveRiver(River river);
        void UpdateRiver(River river);
    }
}
