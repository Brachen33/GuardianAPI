using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianPaternityRelationDTO
    {
        public int Id { get; set; }
        public string PaternityCaseId { get; set; }
        public int ParticipantId { get; set; }      
        public string ADCCode { get; set; }
        public string Relation { get; set; } 
      
        public int CollectionSiteId { get; set; }
    }
}
