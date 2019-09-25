using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models.PSIManager
{
    [Table("users")]
    public class PSIUser
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string UserPass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateUpdated { get; set; }
       
    }
}
