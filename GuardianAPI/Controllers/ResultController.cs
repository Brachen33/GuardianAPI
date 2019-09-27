using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/result")]
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly ILoggerManager _logger;
        private readonly IResultGenerator _resultGen;

        public ResultController(IResultRepository resultRepository, ILoggerManager logger,IResultGenerator resultGen)
        {
            _resultRepository = resultRepository;
            _logger = logger;
            _resultGen = resultGen;
        }

        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_resultRepository.GetResult(id));
        }

        public IActionResult GetGuardianDailyResults()
        {

            return Ok(_resultGen.ResultResponse());

        }




    }
}
