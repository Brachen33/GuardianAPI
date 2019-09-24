using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_company")]
    public class Company
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string IssuedId { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

   //     public IEnumerable<ParticipantPanel> ParticipantPanels { get; set; }

        public Company()
        {
   //         ParticipantPanels = new List<ParticipantPanel>();
        }
    }
}
