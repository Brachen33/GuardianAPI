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
using GuardianAPI.Interfaces.ILoggerManager;

namespace GuardianAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IExternalCreateParticipant _externalCreate;
        private readonly IParticipantRepository _participantRepository;
        private readonly ILoggerManager _logger;

        public ParticipantController(IParticipantRepository participantRepository,ILoggerManager logger, IExternalCreateParticipant externalCreate)
        {
            _participantRepository = participantRepository;
            _externalCreate = externalCreate;
            _logger = logger;
        }   
       
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            ParticipantDTO participantDTO = new ParticipantDTO()
            {
                Participant = await _participantRepository.GetParticipant(id ?? 1),
            };
            return Ok(participantDTO);
        }

        [HttpGet("GetByIssuedId/{issuedId}")]
        public async Task<IActionResult> GetByIssuedId(string issuedId)
        {
            try
            {
                var user = await _participantRepository.GetParticipantByIsssuedId(issuedId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

       

        [HttpGet]
        [Route("getparticipantwithcontact/{id}")]
        public async Task<IActionResult> GetparticipantWithContact(int id)
        {
            return Ok( await _participantRepository.GetParticipantWithContact(id));
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The DTO after it has been successfully saved</returns>
        [HttpPost]
        [Route("guardiancreateparticipant")]
        public async Task<IActionResult> GuardianCreateParticipant([FromBody] GuardianCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    var userAndParticipants = await _externalCreate.GuardianProcess(dto);
                    return Ok(userAndParticipants);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

        //[HttpGet]
        //[Route("SearchParticipants/{sString}")]
        //public async Task<IActionResult> SearchParticipants(string sString)
        //{
        //    try
        //    {
        //        var participants = await _participantRepository.GetParticipantAutocompleteSearch(sString);
        //        return Ok(participants);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"{ex.ToString()}");
        //    }
        //}

        
    }
}
