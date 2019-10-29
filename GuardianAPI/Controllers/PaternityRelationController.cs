using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class PaternityRelationController : Controller
    {
        private readonly IPaternityRelationRepository _paternityRepository;

        public PaternityRelationController(IPaternityRelationRepository paternityRepository)
        {
            _paternityRepository = paternityRepository;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var paternityRelation = await _paternityRepository.GetById(id);

                return Ok(paternityRelation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _paternityRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("GetRelatedPaternityByCaseId/{caseId}")]
        public async Task<IActionResult> GetRelatedPaternityByCaseId(string caseId)
        {
            try
            {
                var paternities = await _paternityRepository.GetRelatedPaternityByCaseId(caseId);
                return Ok(paternities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }


        [HttpGet]
        [Route("GetPaternityByParticipantId/{participantId}")]
        public async Task<IActionResult> GetPaternityByParticipantId(int participantId)
        {
            try
            {
                var paternity = await _paternityRepository.GetPaternityByParticipantId(participantId);
                return Ok(paternity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

        [HttpPost]
        [Route("CreatePaternity")]
        public async Task<IActionResult> CreatePaternity([FromBody] GuardianPaternityRelationDTO paternityRelation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(500, "Invalid model State");
                }
                else
                {
                    var paternity = await _paternityRepository.Create(paternityRelation);

                    return Ok(paternity);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error on Paternity Creation {ex.ToString()}");
            }
        }
    }
}

