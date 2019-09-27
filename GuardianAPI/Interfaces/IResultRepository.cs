using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
   public interface IResultRepository
    {
        Result GetResult(int Id);
        IEnumerable<Result> GetAllResults();
        Result Add(Result result);
        Result Update(Result resultChanges);
        Result GetResultWithDetailById(int id);


    }
}
