using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.DTOs.GeneralDTOs;
using GuardianAPI.Models;

namespace GuardianAPI.Interfaces
{
    public interface IRequisitionRepository
    {
        Task<RequisitionDTO> GetRequisition(int Id);
        Task<IEnumerable<RequisitionDTO>> GetAllRequisitions();
        Task<RequisitionDTO> Add(RequisitionDTO requisition);        
    }
}
