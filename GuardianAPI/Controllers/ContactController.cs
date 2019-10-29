using GuardianAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }


        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = await _contactRepository.GetAllContacts();

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }


        [HttpGet]
        [Route("GetContact")]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                var contact = await _contactRepository.GetContact(id);

                return Ok(contact);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }


        /// <summary>
        /// This Retrieves a record by it's Sql ID and type of record (i.e. "USR" , "PID" etc..)
        /// </summary>
        /// <param name="Contact record"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetContactByRecordId/{recordId}")]
        public async  Task<IActionResult> GetContactForParticipantByRecordId(int recordId)
        {
            try
            {
                var contact = await _contactRepository.GetContactForParticipantByRecordId(recordId);

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }

        [HttpGet]
        [Route("GetContactByTypeAndRecordId/{recordId}/{type}")]
        public async Task<IActionResult> GetContactByTypeAndRecordId(int recordId, string type)
        {
            try
            {
                var contact = await _contactRepository.GetContactByTypeAndRecordId(recordId, type);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }
    }
}
