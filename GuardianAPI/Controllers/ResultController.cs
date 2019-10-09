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
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly ILoggerManager _logger;
        private readonly IResultGenerator _resultGen;
        private readonly IPDFCreatorRepository _pdfCreator;

        public ResultController(IResultRepository resultRepository, ILoggerManager logger, IResultGenerator resultGen, IPDFCreatorRepository pdfCreator)
        {
            _resultRepository = resultRepository;
            _logger = logger;
            _resultGen = resultGen;
            _pdfCreator = pdfCreator;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_resultRepository.GetResult(id));
        }

        [HttpGet]
        [Route("GetGuardianDailyResults")]
        public IActionResult GetGuardianDailyResults()
        {
            return Ok(_resultGen.ResultResponse());
        }

        [HttpGet]
        [Route("getpdf/{resultId}")]
        public IActionResult GuardianResultPDF(int resultId)
        {
            _pdfCreator.GetPDF(PDFType.GuardianExportPDF, resultId);

            return Ok("Export Complete");
        }
    }
}
