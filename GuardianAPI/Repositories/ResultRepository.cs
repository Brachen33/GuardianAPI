using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _context;

        public ResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public Result Add(Result result)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetAllResults()
        {
            return _context.Results;
        }

        public Result GetResult(int Id)
        {
            return _context.Results.Find(Id);
        }

        public Result Update(Result resultChanges)
        {
            throw new NotImplementedException();
        }

        public Result GetResultWithDetailById(int id)
        {
            return _context.Results
                .Include(p => p.Panel)
                .Include(rd => rd.ResultDetails)             
                .FirstOrDefault(r => r.Id == id);
        }
    }
}
