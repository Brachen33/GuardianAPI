using GuardianAPI.Interfaces.PSIManager;
using GuardianAPI.Models;
using GuardianAPI.Models.PSIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories.PSIManager
{
    public class PSIUserRepository : IPSIUserRepository
    {
        private readonly AppDbContext _context;

        public PSIUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PSIUser> GetAll()
        {
            return _context.PSIUsers;
        }

        public PSIUser GetUser(int id)
        {
            return _context.PSIUsers.Find(id);
        }
    }
}
