using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianParticipantPanelDTO
    {
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int UserId { get; set; }
        public int ParticipantId { get; set; }
        public int PanelId { get; set; }
        public int ScheduleType { get; set; }
        public int ScheduleId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int Active { get; set; }       
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
