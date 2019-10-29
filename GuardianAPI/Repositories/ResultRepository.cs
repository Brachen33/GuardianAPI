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

        public async Task<IEnumerable<Result>> GetAllResultsAsync()
        {         
            return await _context.Results.ToListAsync();
        }

        public async Task<Result> GetResult(int Id)
        {
            return await _context.Results.FindAsync(Id);
        }

        public async Task<IEnumerable<Result>> GetResultsByParticipantId(int participantId)
        {
            var results = await _context.Results.Include(x => x.ResultDetails).Where(x => x.ParticipantId == participantId).ToListAsync();

            return results;
        }

        public async Task<Result> GetResultWithDetailById(int id)
        {
            return await _context.Results
                .Include(p => p.Panel)
                .Include(rd => rd.ResultDetails)             
                .FirstOrDefaultAsync(r => r.Id == id);
        }

       


    }
}
