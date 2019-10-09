using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("test1")]
        public void Test1()
        {
            PaternityRelation paternityRelation = new PaternityRelation
            {
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Active = 1,
                PaternityCaseId = "156056",
                Relation = "F",
                ADCCode = "TEST ADC CODE",

            };

            var x = _context.PaternityRelations.Add(paternityRelation);
            _context.SaveChanges();
        }

    }
}