using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    public class Lab
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string LabCode { get; set; }
        public string Name { get; set; }
        public string ResultFooter { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
