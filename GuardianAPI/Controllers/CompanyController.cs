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
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var company = _companyRepository.GetCompany(id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
