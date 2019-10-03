using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class DocumentDataRepository : IDocumentDataRepository
    {
        public IEnumerable<DocumentData> GetAllDocumentData()
        {
            throw new NotImplementedException();
        }

        public DocumentData GetDocumentDataById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
