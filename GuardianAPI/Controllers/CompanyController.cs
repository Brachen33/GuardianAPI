using GuardianAPI.Interfaces;
using GuardianAPI.LoggerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;      

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;         
        }


        [HttpGet]
        [Route("getAllCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                return Ok( await _companyRepository.GetCompanies());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async  Task<IActionResult> GetById(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompany(id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                return Ok(await _companyRepository.GetByName(name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");

            }

        }
    }
}
