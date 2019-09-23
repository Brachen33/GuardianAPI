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

        public IActionResult GetAll()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        public IActionResult GetDetails(int id)
        {
            return Ok(_userRepository.GetUser(id));
        }

        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var createdUser = _userRepository.Add(user);
                return Ok(createdUser);
            }

            return StatusCode(500, "Internal Server Error");


        }






    }
}
