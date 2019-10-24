using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianCreateDTO
    {
        public GuardianUserDTO User { get; set; }



        [Required]        
        public int UserId { get; set; }
      

    }
}
