using GuardianAPI.Interfaces.PSIManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers.PSIManager
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _repository;

        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }


        //[Route("getall")]
        //public IActionResult GetAll()
        //{
        //    var clients = _repository.GetAll();

        //    return Ok(clients);
        //}

        //[Route("getbyid/{id}")]
        //public IActionResult GetbyId(int id)
        //{
        //    var client = _repository.GetClient(id);
        //    return Ok(client);
        //}
    }
}
