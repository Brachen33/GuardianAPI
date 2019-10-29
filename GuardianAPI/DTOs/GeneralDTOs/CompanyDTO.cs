using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.GeneralDTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string IssuedId { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
