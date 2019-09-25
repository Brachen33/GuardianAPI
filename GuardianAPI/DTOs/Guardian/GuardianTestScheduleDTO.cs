using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianTestScheduleDTO
    {       
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int ParticipantId { get; set; }
        public int SiteID { get; set; }
        public DateTime TestDate { get; set; }
        public TimeSpan TestTime { get; set; }
        public int ScheduleType { get; set; }
        public int Active { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public List<GuardianTestPanelDTO> TestPanels { get; set; }

        public GuardianTestScheduleDTO()
        {
            TestPanels = new List<GuardianTestPanelDTO>();
        }
    }
}
