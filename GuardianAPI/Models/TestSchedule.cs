using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_tests_schedule")]
    public class TestSchedule
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int ParticipantId { get; set; }
        public int SiteID { get; set; }
        public DateTime TestDate { get; set; }
        public TimeSpan TestTime { get; set; }
        public int ScheduleType { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public Participant Participant { get; set; }
        public List<TestPanel> TestPanels { get; set; }

        public TestSchedule()
        {
            TestPanels = new List<TestPanel>();
        }

    }
}
