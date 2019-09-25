using GuardianAPI.Interfaces.PSIManager;
using GuardianAPI.Models;
using GuardianAPI.Models.PSIManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories.PSIManager
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients;
        }

        public Client GetClient(int id)
        {
            return _context.Clients.Find(id);
        }

        public Client GetClientWithUsers(int id)
        {
            return _context.Clients.Include(x => x.PSIUsers).FirstOrDefault(x => x.Id == id);
        }
    }
}
