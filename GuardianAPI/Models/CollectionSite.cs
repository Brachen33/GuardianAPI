using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_collection_sites")]
    public class CollectionSite
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string SiteCode { get; set; }
    }
}
