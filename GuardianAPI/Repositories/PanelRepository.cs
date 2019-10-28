using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
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

        public async Task<IEnumerable<Panel>> GetAllPanels()
        {
            var panels = await _context.Panels.ToListAsync();

            return _context.Panels;
        }

        public Panel GetPanel(int Id)
        {
            return _context.Panels.Find(Id);
        }

        public Panel GetPanelByPanelCode(string code)
        {
            var panel = _context.Panels.FirstOrDefault(x => x.LabPanelCode == code);
            return panel;
        }

        public Panel Update(Panel panelChanges)
        {
            var panel = _context.Panels.Attach(panelChanges);
            panel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return panelChanges;
        }



    }
}

