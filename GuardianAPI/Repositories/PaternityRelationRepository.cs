using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class PaternityRelationRepository : IPaternityRelationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;

        public PaternityRelationRepository(AppDbContext context,ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public PaternityRelation GetById(int id)
        {
            return _context.PaternityRelations.Find(id);
        }
    }
}
