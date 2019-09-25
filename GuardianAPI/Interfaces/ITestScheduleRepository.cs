using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface ITestScheduleRepository
    {
        IEnumerable<TestSchedule> GetAll();
        TestSchedule GetById(int id);

    }
}
