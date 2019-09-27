using GuardianAPI.DTOs.Guardian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IResultGenerator
    {
        List<GuardianResultsDailyResponseDTO> ResultResponse();
    }
}
