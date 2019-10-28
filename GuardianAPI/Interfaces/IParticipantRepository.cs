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
        Task<Participant> GetParticipant(int Id);
        Task<IEnumerable<Participant>> GetAllParticipants();
        Task<Participant> Add(Participant participant);
        Task<Participant> Update(Participant participantChanges);     
        Task<bool> DoesParticipantExist(string issuedId);

        // Additional Methods for retrieving a Participant
        Task<Participant> GetParticipantByIsssuedId(string issuedId);
        
      //  Participant GetParticipantWithAll(int Id);
        Task<Participant> GetParticipantWithContact(int id);
        // Retrieves Results and Result Details
       // Participant GetParticipantWithResults(int id);
        //
      

    }
}
