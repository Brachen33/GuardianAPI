using GuardianAPI.DTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GuardianAPI.DTOs.Guardian;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GuardianAPI.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogEntryRepository _logEntry;

        public ParticipantRepository(AppDbContext context, ILogEntryRepository logEntry)
        {
            _context = context;
            _logEntry = logEntry;
        }

        public Participant Add(Participant participant)
        {
            _context.Participants.Add(participant);
            _context.SaveChanges();
            return participant;
        }

       
        public Participant Delete(int id)
        {
            Participant participant = _context.Participants.FirstOrDefault(x => x.Id == id);

            if (participant != null)
            {
                _context.Participants.Remove(participant);
            }
            return participant;
        }

        public IEnumerable<Participant> GetAllParticipants()
        {
            return _context.Participants;
        }

        public Participant GetParticipant(int Id)
        {
            return _context.Participants.Find(Id);
        }

        public Participant GetParticipantByIsssuedId(string issuedId)
        {
            if (issuedId != null)
            {
                return _context.Participants.FirstOrDefault(x => x.IssuedID == issuedId);
            }
            return null;
        }

        //public Participant GetParticipantWithAll(int Id)
        //{
        //    return _context.Participants.Include(x => x.Contact)
        //        .Include(x => x.ParticipantSchedule)
        //        .Include(x => x.Results)
        //        .ThenInclude(x => x.ResultDetails)
        //        .FirstOrDefault(x => x.Id == Id);
        //}

        //public Participant GetParticipantWithContact(int id)
        //{
        //    return _context.Participants.Include(x => x.Contact).FirstOrDefault(x => x.Id == id);
        //}

        //public Participant GetParticipantWithResults(int id)
        //{
        //    return _context.Participants.Include(x => x.Results).FirstOrDefault(x => x.Id == id);
        //}

        public Participant Update(Participant participantChanges)
        {
            var participant = _context.Participants.Attach(participantChanges);
            participant.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return participantChanges;
        }

        public bool DoesParticipantExist(string issuedId)
        {
         return _context.Participants.Any(p => p.IssuedID == issuedId);          
        }



    }
}
