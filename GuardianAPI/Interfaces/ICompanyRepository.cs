using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface ICompanyRepository
    {
       Task<Company> GetCompany(int id);
       Task<IEnumerable<Company>> GetCompanies();
      
    }
}
