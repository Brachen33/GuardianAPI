using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class PanelRepository : IPanelRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;

        public PanelRepository(AppDbContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public Panel Add(Panel panel)
        {
            _context.Panels.Add(panel);
            _context.SaveChanges();
            return panel;
        }

        public async Task<IEnumerable<PanelDTO>> GetAllPanels()
        {           

            var panels = await _context.Panels.ToListAsync();

            return panels.Adapt<IEnumerable<PanelDTO>>();      
        }

        public async Task<PanelDTO> GetPanel(int Id)
        {
            var panel = await _context.Panels.FindAsync(Id);

            return panel.Adapt<PanelDTO>();            
        }

        public async Task<PanelDTO> GetPanelByPanelCode(string code)
        {
            var panel = await _context.Panels.FirstOrDefaultAsync(x => x.LabPanelCode == code);
            return panel.Adapt<PanelDTO>();
        }

        



    }
}

