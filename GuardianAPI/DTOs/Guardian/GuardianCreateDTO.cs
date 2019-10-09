using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.DTOs.Guardian
{
    public class GuardianCreateDTO
    {


        public GuardianUserDTO User { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

    }
}
