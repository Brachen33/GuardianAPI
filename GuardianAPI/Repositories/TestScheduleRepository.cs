using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class TestScheduleRepository : ITestScheduleRepository
    {
        private readonly AppDbContext _context;

        public TestScheduleRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<TestSchedule> GetAll()
        {
            return _context.TestSchedules;
        }

        public TestSchedule GetById(int id)
        {
            return _context.TestSchedules.Find(id);
        }
    }
}
