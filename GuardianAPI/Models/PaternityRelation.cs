using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_paternity_relation")]
    public class PaternityRelation
    {
        public int Id { get; set; }
        public string PaternityCaseId { get; set; }
        public int ParticipantId { get; set; }  
        public int TestId { get; set; }
        public string ADCCode { get; set; }
        public string Relation { get; set; }
        public int CollectionSiteId { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public Participant Participant { get; set; }       
       
       
     

        
    }
}
