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

        public User CreateParticipantFromGuardian(GuardianCreateDTO dto)
        {
            // TODO: Check if the user exists
            // AM Testing           
            var user = _context.Users.Include(x => x.Participants).FirstOrDefault(x => x.Id == dto.User.Id);

            // Map the DTO to the User Object
            user = dto.User.Adapt<User>();

            // Create a new user if the user does not exist
            if (user == null)
            {
                // Set Default properties for the User Record
                user.Active = 1;
                user.DateUpdated = DateTime.Now;
                user.DateCreated = DateTime.Now;

                // User Contact Record
                user.Contact.RecordType = "USR";
                user.Contact.DateCreated = DateTime.Now;
                user.Contact.DateUpdated = DateTime.Now;
                _context.Users.Add(user);
            }


            //   Set the  Default Participant Properites 1 : X
            user.Participants.ForEach(x =>
            {
                // Check if the participant exists
                // If it does, Update the participant
                if (x.Id > 0)
                {
                    x.Active = 1;
                    x.DateUpdated = DateTime.Now;
                }
                else
                {
                    x.Active = 1;
                    x.DateCreated = DateTime.Now;
                    x.DateUpdated = DateTime.Now;
                }
                // TODO: Figure out Contact

                // Add Participant Panel 1 : X
                x.ParticipantPanels.ForEach(pp =>
            {
                if (pp.Id > 0)
                {
                    pp.Active = 1;
                    pp.DateUpdated = DateTime.Now;
                    pp.ParticipantId = x.Id;
                    pp.UserId = user.Id;
                }
                else
                {
                    pp.Active = 1;
                    // pp.DateCreated = DateTime.Now;
                    pp.DateUpdated = DateTime.Now;

                    // TODO: For testing only
                    pp.StartDate = DateTime.Now;
                    pp.EndDate = DateTime.Now.AddMonths(2);
                }
            });

                // Set Participant Schedule 1 : 1
                if (x.ParticipantSchedule != null)
                {
                    x.ParticipantSchedule.Active = 1;
                }
                else
                {
                    x.ParticipantSchedule.DateCreated = DateTime.Now;
                }


                // Set the Participants Requisitions 1 : X
                x.Requisitions.ForEach(r =>
            {
                r.ReqDate = DateTime.Now.Date;
                r.ReqTime = DateTime.Now.TimeOfDay;
                r.Active = 1;
                r.DateCreated = DateTime.Now;
                r.DateUpdated = DateTime.Now;
            });

                // TODO: set the if condition for if it is a manual test
                if (x.TestSchedules.Count > 0)
                {
                    // Set the Test Schedule if it is a manual test
                    x.TestSchedules.ForEach(ts =>
                {
                    ts.Active = 1;
                    ts.DateCreated = DateTime.Now;
                    ts.DateUpdated = DateTime.Now;
                    ts.CompanyId = x.CompanyID;
                    ts.RegionId = x.RegionID;
                    ts.ParticipantId = x.Id;

                    // Set the Tests Panels if it is a manual test
                    ts.TestPanels.ForEach(tp =>
                    {
                        tp.ScheduleType = 1;
                        tp.ScheduleModel = "M";
                        tp.OrderedBy = user.Id;

                        // Get the panel object by the test panel panel code
                        var panel = _context.Panels.FirstOrDefault(p => p.LabPanelCode == tp.PanelCode);
                        // Assign properties from the panel object
                        tp.PanelID = panel.Id;
                        tp.LabCode = panel.LabCode;
                        tp.PanelDescription = panel.Description;
                    });
                });
                }
            });
            _context.SaveChanges();
            return user;
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
