using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_schedules")]
    public class Schedule
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int SiteId { get; set; }
        public TimeSpan? CallTimeStart { get; set; }
        public TimeSpan? CallTimeEnd  { get; set; }
        public TimeSpan? TestTimeStart { get; set; }
        public TimeSpan? TestTimeEnd { get; set; }
        public int TestTimeInc { get; set; }
        public int ScheduleId { get; set; }
        public TimeSpan? TimeOpen { get; set; }
        public TimeSpan? TimeClosed { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

    }
}
