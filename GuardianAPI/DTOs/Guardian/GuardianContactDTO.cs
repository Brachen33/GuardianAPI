using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianContactDTO
    {        
        [Range(0, 11, ErrorMessage = "RecordId for Contact is too many characters ")]
        public int RecordID { get; set; }
        [Required]
        [MaxLength(3,ErrorMessage ="Record Type Max characters is 3")]
       public string RecordType { get; set; }
        [MaxLength(40,ErrorMessage ="Address1 max characters is 40")]
        public string Address1 { get; set; }
        [MaxLength(30,ErrorMessage ="City max characters is 30")]
        public string City { get; set; }
        [MaxLength(2,ErrorMessage = "State max characters is 2")]
        public string State { get; set; }
        [MaxLength(6,ErrorMessage = "Zip1 max characters is 6")]
        public string Zip1 { get; set; }
        [JsonProperty(PropertyName = "emailAddress")]
        [MaxLength(50,ErrorMessage ="Email max characters is 50")]
        public string Email { get; set; }
        [MaxLength(14,ErrorMessage ="Phone max characters is 14")]
        public string Phone { get; set; }             

    }
}
