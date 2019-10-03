using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IDocumentDataRepository
    {
        IEnumerable<DocumentData> GetAllDocumentData();
        DocumentData GetDocumentDataById(int id);
    }
}
