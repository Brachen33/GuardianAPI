using GuardianAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class PanelController : Controller
    {
        private readonly IPanelRepository _panelRepository;

        public PanelController(IPanelRepository panelRepository)
        {
            _panelRepository = panelRepository;
        }


        [HttpGet]
        [Route("getAllPanels")]
        public async Task<IActionResult> GetAllPanels()
        {
            try
            {
                var panels = await _panelRepository.GetAllPanels();

                return Ok(panels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.ToString()}");

            }

        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var panel = _panelRepository.GetPanel(id);

                return Ok(panel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
