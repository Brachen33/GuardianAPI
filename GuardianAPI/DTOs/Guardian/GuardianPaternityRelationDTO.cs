using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianPaternityRelationDTO
    {
        public int Id { get; set; }
        [StringLength(20,ErrorMessage ="Max Paternity Case Id is 20 characters")]
        public string PaternityCaseId { get; set; }
        public int ParticipantId { get; set; }
        [StringLength(45,ErrorMessage ="Max ADC Code length is 45 characters")]
        public string ADCCode { get; set; }
        [StringLength(45,ErrorMessage ="Max Relation length is 45 characters")]
        public string Relation { get; set; } 
      
        public int CollectionSiteId { get; set; }
    }
}
