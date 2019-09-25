using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models.PSIManager
{
    [Table("clients")]
    public class Client
    {
        public int Id { get; set; }
        public string ExternalAccountId { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SubWebsite { get; set; }
        public string DatabaseName { get; set; }
        public string AppCheckCode { get; set; }
        public int Active { get; set; }

        public List<PSIUser> PSIUsers { get; set; }

        public Client()
        {
            PSIUsers = new List<PSIUser>();
        }

    }
}
