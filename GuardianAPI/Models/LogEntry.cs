using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_log_entries")]
    public class LogEntry
    {
        public int Id { get; set; }
        public int ClientID { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string IPAddress { get; set; }
        public string ActionCode { get; set; }
        public string Description { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }


        public LogEntry()
        {

        }

    }
}
