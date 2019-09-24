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

        public User CreateParticipantFromGuardian(GuardianCreateDTO dto)
        {
            // TODO: Check if the user exists
            var user = _context.Users.FirstOrDefault(x => x.Id == dto.User.Id);


            // Create a new user if the user does not exist
            if (user == null)
            {
                // Map the DTO to the User Object
                var createdUser = dto.User.Adapt<User>();

                // Set Default properties for the User Record
                createdUser.Active = 1;
                createdUser.DateUpdated = DateTime.Now;
                createdUser.DateCreated = DateTime.Now;

                // User Contact Record
                createdUser.Contact.RecordType = "USR";
                createdUser.Contact.DateCreated = DateTime.Now;
                createdUser.Contact.DateUpdated = DateTime.Now;


                //   Set the  Default Participant Properites 1 : X
                createdUser.Participants.ForEach(x =>
                {
                    x.DateCreated = DateTime.Now;
                    x.DateUpdated = DateTime.Now;
                    x.Active = 1;


                    // Add Participant Panel 1 : X
                    x.ParticipantPanels.ForEach(pp => {
                        pp.Active = 1;
                        pp.DateCreated = DateTime.Now;
                        pp.DateUpdated = DateTime.Now;
                        // TODO: For testing only
                        pp.StartDate = DateTime.Now;
                        pp.EndDate = DateTime.Now.AddMonths(2);
                    });

                    // Set Participant Schedule 1 : 1
                    x.ParticipantSchedule.Active = 1;
                    x.ParticipantSchedule.DateCreated = DateTime.Now;


                    // Set the Participants Requisitions 1 : X
                    x.Requisitions.ForEach(r =>
                    {
                        r.ReqDate = DateTime.Now.Date;
                        r.ReqTime = DateTime.Now.TimeOfDay;
                        r.Active = 1;
                        r.DateCreated = DateTime.Now;
                        r.DateUpdated = DateTime.Now;
                    });
                });
                _context.Users.Add(createdUser);
            }
                        
            _context.SaveChanges();

            // Log the record in the log entry database
            LogEntry logEntry = new LogEntry
            {
                UserId = user.Id,
                ActionDateTime = DateTime.Now,
                IPAddress = "TODO",
                ActionCode = "INSERT",
                Description = $"Created User # {user.Id}",
                RecordType = "USR"
            };

            return null;
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
    }
}
