using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianTestPanelDTO
    {     
        [Required]
        public int PanelID { get; set; }        
        public string LabCode { get; set; }
        public string PanelCode { get; set; }
        public int ScheduleType { get; set; }
        public string ScheduleModel { get; set; }
        public string PanelDescription { get; set; }
        public int CompanyID { get; set; }
        public int RegionID { get; set; }      
    }
}
