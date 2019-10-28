using GuardianAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository _regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }


        [HttpGet]
        [Route("getAllRegions")]
        public IActionResult GetAllRegions()
        {
            try
            {
                return Ok(_regionRepository.Regions());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");
            }


        }

        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var region = _regionRepository.GetRegion(id);

                return Ok(region);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

    }
}
