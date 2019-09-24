using GuardianAPI.BLL;
using GuardianAPI.DTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GuardianAPI.DTOs.Guardian;
using Microsoft.AspNetCore.Authorization;

namespace GuardianAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantController(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

   
        [HttpGet("index")]
        public IActionResult Index()
        {
            var model = _participantRepository.GetAllParticipants();
            return Ok(model);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int? id)
        {
            ParticipantDTO participantDTO = new ParticipantDTO()
            {
                Participant = _participantRepository.GetParticipant(id ?? 1),
            };
            return Ok(participantDTO);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(Participant participant)
        {
            if (ModelState.IsValid)
            {
                Participant newParticipant = _participantRepository.Add(participant);
                return RedirectToAction("details", new { id = newParticipant.Id });
            }
            return View();
        }


        [HttpGet]
        [Route("getpdf/{participantId}")]
        public IActionResult GuardianResultPDF(int participantId)
        {
            var participant = _participantRepository.GetParticipant(participantId);
            PDFCreator.GetPDF(PDFType.GuardianExportPDF, participant);
            // PDFCreator.CreateGuardianPDFResult(participantId,);
            return Ok("Export Complete");
        }



        //[HttpGet]
        //[Route("getparticipantwithall/{id}")]
        //public IActionResult GetParticipantWithAll(int id)
        //{
        //    return Ok(_participantRepository.GetParticipantWithAll(id));
        //}

        //[HttpGet]
        //[Route("getparticipantwithcontact/{id}")]
        //public IActionResult GetparticipantWithContact(int id)
        //{
        //    return Ok(_participantRepository.GetParticipantWithContact(id));
        //}

        //[HttpGet]
        //[Route("getparticipantwithresults/{id}")]
        //public IActionResult GetParticipantWithResults(int id)
        //{
        //    return Ok(_participantRepository.GetParticipantWithResults(id));
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The DTO after it has been successfully saved</returns>
        [HttpPost]
        [Route("guardiancreateparticipant")]
        public IActionResult GuardianCreateParticipant([FromBody] GuardianCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var createdParticipant = _participantRepository.CreateParticipantFromGuardian(dto);
                return Ok(createdParticipant);
            }
        }





    }
}
