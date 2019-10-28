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

        public async Task<Participant> Add(Participant participant)
        {
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
            return participant;
        }

       
       
        public async Task<IEnumerable<Participant>> GetAllParticipants()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<Participant> GetParticipant(int Id)
        {
            return await _context.Participants.FindAsync(Id);
        }

        public async Task<Participant> GetParticipantByIsssuedId(string issuedId)
        {
            if (issuedId != null)
            {
                return await _context.Participants.FirstOrDefaultAsync(x => x.IssuedID == issuedId);
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

        public async Task<Participant> GetParticipantWithContact(int id)
        {
            return await _context.Participants.Include(x => x.Contact).FirstOrDefaultAsync(x => x.Id == id);
        }

        //public Participant GetParticipantWithResults(int id)
        //{
        //    return _context.Participants.Include(x => x.Results).FirstOrDefault(x => x.Id == id);
        //}

        public async Task<Participant> Update(Participant participantChanges)
        {
            var participant =  _context.Participants.Attach(participantChanges);
            participant.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return participantChanges;
        }

        public async Task<bool> DoesParticipantExist(string issuedId)
        {
         return await _context.Participants.AnyAsync(p => p.IssuedID == issuedId);          
        }



    }
}
