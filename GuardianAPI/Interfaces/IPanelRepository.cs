using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IPanelRepository
    {
        Panel GetPanel(int Id);
        IEnumerable<Panel> GetAllPanels();
        Panel Add(Panel panel);
        Panel Update(Panel panelChanges);
        Panel GetPanelByPanelCode(string code);
       
    }
}
