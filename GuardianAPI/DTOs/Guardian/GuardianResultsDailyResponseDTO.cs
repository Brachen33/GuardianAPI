using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianResultsDailyResponseDTO
    {
        public int Id { get; set; }
        public string PID_2_1 { get; set; }
        public string PID_5_1 { get; set; }
        public string PID_5_2 { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
