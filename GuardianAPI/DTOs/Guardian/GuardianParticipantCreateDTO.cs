using GuardianAPI.DTOs.Guardian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs
{
    public class GuardianParticipantCreateDTO
    {
        public int Id { get; set; }
        public int CompanyID { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "You must enter a Social Security Number")]
        public string IssuedID { get; set; }
        [StringLength(35, ErrorMessage = "The maximum number of characters for a first name is 35")]
        public string FirstName { get; set; }
        [StringLength(35, ErrorMessage = "The maximum number of characters for a last name is 35")]
        public string LastName { get; set; }
        [StringLength(1, ErrorMessage = "A middle initial can only be one character")]
        public string MI { get; set; }

        public int CaseManagerID { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
       
        public int RegionID { get; set; }

       
     //   public GuardianContactDTO Contact { get; set; }
        public GuardianParticipantScheduleDTO ParticipantSchedule { get; set; }
        public IEnumerable<GuardianParticipantPanelDTO> ParticipantPanels { get; set; }
        public IEnumerable<GuardianRequisitionDTO> Requisitions { get; set; }
        public IEnumerable<GuardianTestScheduleDTO> TestSchedules { get; set; }  
        public IEnumerable<GuardianPaternityRelationDTO> PaternityRelations { get; set; }
       

        public GuardianParticipantCreateDTO()
        {
     //       Contact = new GuardianContactDTO();
            ParticipantSchedule = new GuardianParticipantScheduleDTO();
            ParticipantPanels = new List<GuardianParticipantPanelDTO>();
            Requisitions = new List<GuardianRequisitionDTO>();
            TestSchedules = new List<GuardianTestScheduleDTO>();
            PaternityRelations = new List<GuardianPaternityRelationDTO>();
        }
    }
}
