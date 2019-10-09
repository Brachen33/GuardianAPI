﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianRequisitionDTO
    { 
        public int CompanyId { get; set; }       
        public int RegionId { get; set; }
      //  public DateTime ReqDate { get; set; } 
      //  public TimeSpan ReqTime { get; set; } 
      //  public int ParticipantId { get; set; }
        public string ParticipantIssuedId { get; set; }
        public string ParticipantFName { get; set; }
        public string ParticipantLName { get; set; }
        public string RecordType { get; set; }
        public string CaseNumber { get; set; }
        public int ProfileCode { get; set; }
        public string ProfileDescription { get; set; }
        public int ScheduleId { get; set; }
        public string ScheduleModel { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CaseManagerId { get; set; }
        public int ScheduleFreq { get; set; }
        public int ScheduleSunday { get; set; }
        public int ScheduleMonday { get; set; }
        public int ScheduleTuesday { get; set; }
        public int ScheduleWednesday { get; set; }
        public int ScheduleThursday { get; set; }
        public int ScheduleFriday { get; set; }
        public int ScheduleSaturday { get; set; }
        public int CaseManagerProfileId { get; set; }
        public string Notes { get; set; }
        public string CaseManagerFName { get; set; }
        public string CaseManagerLName { get; set; }
        public string Title { get; set; }
        public int SubSpecimen { get; set; }
        public int Observe { get; set; }     
    }
}
