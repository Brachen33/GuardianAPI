using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
   public interface IResultRepository
    {
        Task<Result> GetResult(int Id);
        Task<IEnumerable<Result>> GetAllResultsAsync();
    
        Task<Result> GetResultWithDetailById(int id);
        Task<IEnumerable<Result>> GetResultsByParticipantId(int participantId);
    }
}
