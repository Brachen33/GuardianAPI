﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_panel")]
    public class Panel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string OldPanelCode { get; set; }
        public string OldCOCCode { get; set; }
        public int LabId { get; set; }
        public string LabCode { get; set; }
        public string LabPanelCode { get; set; }
        public string PanelType { get; set; }
        public int Observable { get; set; }
        public string SpecimenType { get; set; }
        public decimal? Price { get; set; }
        public decimal? ConfirmRate { get; set; }
        public decimal? CollectFee { get; set; }
        public int WaitDays { get; set; }
        public int AutoConfirm { get; set; }
        public int Active { get; set; }

        public List<TestPanel> TestPanels { get; set; }
        public List<Result> Results { get; set; }
       

        public Panel()
        {
            Results = new List<Result>();
            TestPanels = new List<TestPanel>();
        }


    }
}
