using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _context;

        public RegionRepository(AppDbContext context)
        {
            _context = context;
        }


        public Region Add(Region region)
        {
            throw new NotImplementedException();
        }

        public Region GetRegion(int Id)
        {
            return _context.Regions.Find(Id);
        }

        public IEnumerable<Region> Regions()
        {
            return _context.Regions;
        }

        public Region Update(Region regionChanges)
        {
            throw new NotImplementedException();
        }
    }
}
