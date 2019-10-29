using GuardianAPI.DTOs;
using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;

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

        public async Task<Participant> GetParticipantWithContact(int id)
        {
            return await _context.Participants.Include(x => x.Contact).FirstOrDefaultAsync(x => x.Id == id);
        }       

        public async Task<Participant> Update(Participant participantChanges)
        {
            var participant = _context.Participants.Attach(participantChanges);
            participant.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return participantChanges;
        }

        public async Task<bool> DoesParticipantExist(string issuedId)
        {
            return await _context.Participants.AnyAsync(p => p.IssuedID == issuedId);
        }

        public async Task<IEnumerable<GuardianAPI.DTOs.GeneralDTOs.ParticipantDTO>> GetParticipantAutocompleteSearch(string sString)
        {
            IEnumerable<Participant> participants = new List<Participant>();
            var isNumeric = int.TryParse(sString, out int n);

            if (!isNumeric)
            {
                participants = await _context.Participants.Where(p => p.FirstName.Contains(sString) || p.LastName.Contains(sString) || (p.FirstName + " " + p.LastName).Contains(sString) || (p.LastName + " " + p.FirstName).Contains(sString) || (p.LastName + ", " + p.FirstName).Contains(sString)).ToListAsync();
            }
            else
            {
                participants = await _context.Participants.Where(p => p.IssuedID.Contains(sString)).ToListAsync();
            }
            return participants.Adapt<IEnumerable<DTOs.GeneralDTOs.ParticipantDTO>>().OrderBy(x => x.LastName);          
        }



    }
}
