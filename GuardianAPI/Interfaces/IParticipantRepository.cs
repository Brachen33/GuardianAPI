using GuardianAPI.DTOs;
using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IParticipantRepository
    {
        Participant GetParticipant(int Id);
        IEnumerable<Participant> GetAllParticipants();
        Participant Add(Participant participant);
        Participant Update(Participant participantChanges);
        Participant Delete(int id);
        bool DoesParticipantExist(string issuedId);

        // Additional Methods for retrieving a Participant
        Participant GetParticipantByIsssuedId(string issuedId);
        
      //  Participant GetParticipantWithAll(int Id);
      //  Participant GetParticipantWithContact(int id);
        // Retrieves Results and Result Details
       // Participant GetParticipantWithResults(int id);
        //
      

    }
}
