using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.Models;

namespace GuardianAPI.Interfaces
{
    public interface IRequisitionRepository
    {
        Requisition GetRequisition(int Id);
        IEnumerable<Requisition> GetAllRequisitions();
        Requisition Add(Requisition requisition);
        Requisition Update(Requisition requisitionChanges);
    }
}
