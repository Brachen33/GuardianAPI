using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface  IDocumentRepository
    {
        IEnumerable<Document> GetDocuments();
        Document GetDocumentbyId(int id);
        Task<byte[]> GetLatestParticipantPhotoByParticipantId(int participantId);
        byte[] GetPhotoByResult(int participantId);


    }
}
