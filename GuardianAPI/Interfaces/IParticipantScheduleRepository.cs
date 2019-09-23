using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IParticipantScheduleRepository
    {
        ParticipantSchedule GetParticipantSchedule(int Id);
        IEnumerable<ParticipantSchedule> GetAllParticipantSchedules();
        ParticipantSchedule Add(ParticipantSchedule participantSchedule);
        ParticipantSchedule Update(ParticipantSchedule participantScheduleChanges);
    }
}
