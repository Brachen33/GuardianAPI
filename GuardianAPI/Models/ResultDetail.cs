using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    [Table("td_result_details")]
    public class ResultDetail
    {
        public int Id { get; set; }
        public int ResultID { get; set; }
        public string LineType { get; set; }
        public int ItemIndex { get; set; }
        public string MSH_3_1 { get; set; }
        public int? OBR_1_1 { get; set; }
        public string OBR_2_1 { get; set; }
        public string OBR_4_1 { get; set; }
        public string OBR_4_2 { get; set; }
        public string OBR_25_1 { get; set; }
        public int? OBX_1_1 { get; set; }
        public string OBX_3_2 { get; set; }
        public string OBX_7_1 { get; set; }
        public string OBX_8_1 { get; set; }
        public int? NTE_1_1 { get; set; }
        public string NTE_3_1 { get; set; }

    }
}
