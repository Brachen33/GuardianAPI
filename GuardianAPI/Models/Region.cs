using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_region")]
    public class Region
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string IssuedId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }

        public IEnumerable<ParticipantPanel> ParticipantPanels { get; set; }


        public Region()
        {
            ParticipantPanels = new List<ParticipantPanel>();
        }
    }
}
