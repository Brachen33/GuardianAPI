using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_users")]
    public class User
    {
        public int Id { get; set; }
     //   [Range(1,int.MaxValue,ErrorMessage = "Please select a Company.")]
        public int CompanyId { get; set; }

        public int ParentUserId { get; set; }

      //  [Range(1,int.MaxValue,ErrorMessage = "Please select a Region.")]
        public int RegionId { get; set; }

        public int? CollectionSiteId { get; set; }

        [StringLength(20, ErrorMessage = "Please provide a user pin")]
     //   [Required]
        public string UserPin { get; set; }

        [StringLength(32,ErrorMessage = "Please provide a Password")]
        public string UserPass { get; set; }

        [StringLength(10)]
        public string UserType { get; set; }

        [Required]
     //   [Range(3,int.MaxValue,ErrorMessage ="Please provide a role")]
        public int RoleId { get; set; }

        [StringLength(35,ErrorMessage = "The Maximum amount of characters for the first name is 35")]
        public string FirstName { get; set; }

        [StringLength(35,ErrorMessage = "The Maximum amount of characters for the first name is 35")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "The Maximum amount of characters for an Alias is 20")]
        public string Alias { get; set; }

        [StringLength(1,ErrorMessage ="A middle initial can only be one character")]
        public string MI { get; set; }

        [StringLength(2,ErrorMessage ="The Maximum amount of characters for Initials is 2")]
        public string Initials { get; set; }

        [StringLength(1,ErrorMessage ="Ethnicity can only be one character")]
        public string Ethnicity { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [StringLength(30,ErrorMessage = "The Maximum amount of characters for the a job title is 35")]
        public string JobTitle { get; set; }

        [StringLength(10,ErrorMessage = "The Maximum amount of characters for a site code is 10")]
     //   [Required]
        public string SiteCode { get; set; }

        public int? OldProfileID { get; set; }
        public string OldDistrictID { get; set; }
        public int? OldSupID { get; set; }
        public int? OldAccessID { get; set; }
        public int? OldLab { get; set; }
        public int? LoginAttempts { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public int TempPass { get; set; }
        public DateTime? TempPassDate { get; set; }
        public int ShareInfo { get; set; }

        [StringLength(4,ErrorMessage ="An App Pin must be 4 characters")]
        public string AppPin { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public List<Participant> Participants { get; set; }           
    //    public Contact Contact { get; set; }

        public User()
        {
            List<Participant> Participants = new List<Participant>();
   //         Contact = new Contact();
        }
    }
}
