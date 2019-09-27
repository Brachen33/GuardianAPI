using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_results")]
    public class Result
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int? LabId { get; set; }
        public string SpecimenType { get; set; }
        public string OBR_3_1 { get; set; }
        public string MSH_3_1 { get; set; }
        public string PID_2_1 { get; set; }
        public string PID_5_1 { get; set; }
        public string PID_5_2 { get; set; }
        public DateTime? PID_7_1 { get; set; }
        public string PID_8_1 { get; set; }
        public string OBR_2_1 { get; set; }
        public string OBR_4_1 { get; set; }
        public string OBR_4_2 { get; set; }
        [DataType(DataType.Date)]
        public DateTime OBR_7_1 { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan OBR_7_2 { get; set; }
        [DataType(DataType.Date)]
        public DateTime OBR_14_1 { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan OBR_14_2 { get; set; }
        [DataType(DataType.Date)]
        public DateTime OBX_14_1 { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan OBX_14_2 { get; set; }
        public string Frozen { get; set; }
        public string Abnormal { get; set; }
        public string Corrected { get; set; }
        public int? TestID { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        // Nav Properties       
        public List<ResultDetail> ResultDetails { get; set; }
        public Panel Panel { get; set; }

        public Result()
        {
            IEnumerable<ResultDetail> ResultDetails = new List<ResultDetail>();
            Panel = new Panel();
        }





    }
}
