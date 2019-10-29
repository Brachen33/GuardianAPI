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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _userRepository.GetAllUsers());
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userRepository.GetUser(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }           
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                var createdUser = _userRepository.Add(user);
                return Ok(createdUser);
            }
            return StatusCode(500, "Internal Server Error");
        }

        [HttpGet]
        [Route("GetUserWithParticipantsById/{id}")]
        public async Task<IActionResult> GetUserWithParticipantsById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserWithParticipantsByIdAsync(id);

                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }
        }






    }
}
