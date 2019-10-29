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
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var panel = await _panelRepository.GetPanel(id);

                return Ok(panel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetPanelByPanelCode")]
        public async Task<IActionResult> GetPanelByPanelCode(string panelCode)
        {
            try
            {
                var panel = await _panelRepository.GetPanelByPanelCode(panelCode);
                return Ok(panel);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"{ex.ToString()}");
            }
        }
    }
}
