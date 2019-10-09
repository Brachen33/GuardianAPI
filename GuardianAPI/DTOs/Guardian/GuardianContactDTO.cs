using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianContactDTO
    {        
        public int RecordID { get; set; }
        public string RecordType { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip1 { get; set; }
        [JsonProperty(PropertyName = "emailAddress")]
        public string Email { get; set; }
        public string Phone { get; set; }          
     

    }
}
