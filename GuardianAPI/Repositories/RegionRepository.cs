using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
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



        public async Task<RegionDTO> GetByName(string name)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Name == name);

            return region.Adapt<RegionDTO>();
        }

        public async Task<RegionDTO> GetRegion(int Id)
        {
            var region = await _context.Regions.FindAsync(Id);

            return region.Adapt<RegionDTO>();           
        }

        public async Task<IEnumerable<RegionDTO>> Regions()
        {
            var regions = await _context.Regions.ToListAsync();

            return regions.Adapt<IEnumerable<RegionDTO>>();           
        }

    }
}
