using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_contact")]
    public class Contact
    {
        public int Id { get; set; }
        public int RecordID { get; set; }
        public string RecordType { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip1 { get; set; }
        [JsonProperty(PropertyName = "emailAddress")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }        

        public User User { get; set; }
        public Participant Participant { get; set; }
    }
}
