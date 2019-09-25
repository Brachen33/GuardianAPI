using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class TestPanelRepository : ITestPanelRepository
    {
        private readonly AppDbContext _context;

        public TestPanelRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TestPanel> GetAll()
        {
            return _context.TestPanels;
        }

        public TestPanel GetById(int id)
        {
            return _context.TestPanels.Find(id);
        }
    }
}
