using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IResultDetailRepository
    {
        ResultDetail GetResultDetail(int Id);
        IEnumerable<ResultDetail> GetAllResultDetails();
        ResultDetail Add(ResultDetail resultDetail);
        ResultDetail Update(ResultDetail resultDetailChanges);
        ResultDetail Delete(int id);
    }
}
