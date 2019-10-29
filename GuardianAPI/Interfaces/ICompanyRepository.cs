using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface ICompanyRepository
    {
       Task<CompanyDTO> GetCompany(int id);
       Task<IEnumerable<CompanyDTO>> GetCompanies();
        Task<CompanyDTO> GetByName(string name);
      
    }
}
