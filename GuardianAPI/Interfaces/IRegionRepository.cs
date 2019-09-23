using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IRegionRepository
    {
        Region GetRegion(int Id);
        IEnumerable<Region> Regions();
        Region Add(Region region);
        Region Update(Region regionChanges);        
    }
}
