using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Interfaces.PSIManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers.PSIManager
{
    [Route("api/[controller]")]
    public class PSIUserController : Controller
    {
        private readonly IPSIUserRepository _repository;
        private readonly ILoggerManager _logger;

        public PSIUserController(IPSIUserRepository repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInfo("are we here?");


            try
            {
                var psiUser = _repository.GetUser(id);

                return Ok(psiUser);
            }
            catch (Exception ex)
            {
                return Ok($"internal server errror " + ex.ToString());
            }
        }


        
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var all =  _repository.GetAll();
                return Ok(all);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "internal server error");
            }
        }
    }
}

