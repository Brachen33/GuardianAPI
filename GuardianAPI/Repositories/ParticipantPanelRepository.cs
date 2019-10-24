using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class ParticipantPanelRepository : IParticipantPanelRepository
    {
        public ParticipantPanel Add(ParticipantPanel participantPanel)
        {
            throw new NotImplementedException();
        }

        public ParticipantPanel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParticipantPanel> GetAllParticipantPanels()
        {
            throw new NotImplementedException();
        }

        public ParticipantPanel GetParticipant(int Id)
        {
            throw new NotImplementedException();
        }

        public ParticipantPanel Update(ParticipantPanel participantPanelChanges)
        {
            throw new NotImplementedException();
        }


        public List<ParticipantPanel> LoadParticipantPanelsForParticipant(List<ParticipantPanel> pPanels)
        {


            return pPanels; 
        }

    }
}
