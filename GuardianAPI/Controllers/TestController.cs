using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.LoggerService;
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
        [Route("Test1")]
        public string Test1()
        {
            var x = "Stop here";
            
            return "This is only a test1";
        }




    }
}