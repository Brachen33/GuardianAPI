using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_user_roles")]
    public class UserRole
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public int RoleLevel { get; set; }
        public string Name { get; set; }
        public string RoleCode { get; set; }
    }
}
