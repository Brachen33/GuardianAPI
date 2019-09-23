﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_participant_schedule")]
    public class ParticipantSchedule
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int ScheduleId { get; set; }
        [Required]
        [StringLength(1,ErrorMessage ="Schedule Model can only be one character")]
        public string ScheduleModel { get; set; }
        public int Frequency { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Active { get; set; }
        public DateTime? DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
     
    }
}
