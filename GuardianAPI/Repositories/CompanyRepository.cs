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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();

            return  companies.Adapt<IEnumerable<CompanyDTO>>();           
        }

        public async Task<CompanyDTO> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            return company.Adapt<CompanyDTO>();
        }

        public async Task<CompanyDTO> GetByName(string name)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Name == name);

            return company.Adapt<CompanyDTO>();       

        }
    }
}
