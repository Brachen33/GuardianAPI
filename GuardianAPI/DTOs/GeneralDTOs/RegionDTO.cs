using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.GeneralDTOs
{
    public class RegionDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string IssuedId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}
