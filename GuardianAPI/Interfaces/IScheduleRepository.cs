using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule GetSchedule(int Id);
        IEnumerable<Schedule> GetAllSchedules();
        Schedule Add(Schedule schedule);
        Schedule Update(Schedule scheduleChanges);
    }
}
