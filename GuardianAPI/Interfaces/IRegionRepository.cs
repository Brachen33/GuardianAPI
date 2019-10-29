using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IRegionRepository
    {
        Task<RegionDTO> GetRegion(int Id);
        Task<IEnumerable<RegionDTO>> Regions();     

        Task<RegionDTO> GetByName(string name);
    }
}
