using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.GeneralDTOs
{
    public class ParticipantDTO
    {
        public int Id { get; set; }
        [Required]
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
        public int CaseManagerId { get; set; }
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Active { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int RegionID { get; set; }
    }
}
