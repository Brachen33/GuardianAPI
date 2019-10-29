using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IPanelRepository
    {
        Task<PanelDTO> GetPanel(int Id);
        Task<IEnumerable<PanelDTO>> GetAllPanels();
        Panel Add(Panel panel);
        //Panel Update(Panel panelChanges);
        Task<PanelDTO> GetPanelByPanelCode(string code);
       
    }
}
