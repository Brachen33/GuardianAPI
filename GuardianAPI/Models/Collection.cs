using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_collection")]
    public class Collection
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? ParticipantId { get; set; }
        public int? SiteId { get; set; }
        public int? SiteRegionId { get; set; }
        public string SiteRegionCode { get; set; }
        public DateTime DateCollected { get; set; }
        public TimeSpan TimeCollected { get; set; }
        public int? OldCaseManagerId { get; set; }
        public int? CaseManagerId { get; set; }
        public string CaseManagerName { get; set; }
        public string CaseManagerFName { get; set; }
        public string CaseManagerLName { get; set; }
        public string CaseManagerRegionCode { get; set; }
        public int? CaseManagerRegionId { get; set; }
        public string SiteName { get; set; }
        public int? TestId { get; set; }
        public string PanelCode { get; set; }
    }
}
