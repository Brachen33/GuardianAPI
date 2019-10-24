using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IParticipantPanelRepository
    {
        ParticipantPanel GetParticipant(int Id);
        IEnumerable<ParticipantPanel> GetAllParticipantPanels();
        ParticipantPanel Add(ParticipantPanel participantPanel);
        ParticipantPanel Update(ParticipantPanel participantPanelChanges);
        ParticipantPanel Delete(int id);
      
    }
}
