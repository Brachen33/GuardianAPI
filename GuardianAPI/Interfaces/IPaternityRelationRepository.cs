using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IPaternityRelationRepository
    {
        Task<PaternityRelation> GetById(int id);
        Task<IEnumerable<PaternityRelation>> GetAll();
        Task<PaternityRelation> Create(GuardianPaternityRelationDTO paternityDTO);

        Task<IEnumerable<PaternityRelation>> GetRelatedPaternityByCaseId(string caseId);
        Task<PaternityRelation> GetPaternityByParticipantId(int participantId);            
    }
}
