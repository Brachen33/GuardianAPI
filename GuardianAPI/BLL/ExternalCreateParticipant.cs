using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardianAPI.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GuardianAPI.BLL
{
    public class ExternalCreateParticipant : IExternalCreateParticipant
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _user;

        public ExternalCreateParticipant(AppDbContext context, ILoggerManager logger, IUserRepository user)
        {
            _context = context;
            _user = user;
            _logger = logger;
        }

        public User CreateParticipant(GuardianCreateDTO dto)
        {

            dto.User.Participants.Add(new Participant
            {
                

            });


            //// Convert dto to user Map
            //var userMap = dto.User.Adapt<User>();
            
            ////// AM Testing 
            //var existingUser = _context.Users
            //     .Where(u => u.Id == dto.User.Id)
            //    // .Include(x => x.Contact)
            //     .Include(p => p.Participants)
            //     .SingleOrDefault();

           

            //if (existingUser != null)
            //{
            //    _context.Entry(existingUser).CurrentValues.SetValues(dto.User);

            //    // Delete children
            //    foreach (var existingParticipant in existingUser.Participants.ToList())
            //    {
            //        if (!dto.User.Participants.Any(c => c.Id == existingParticipant.Id))
            //            _context.Participants.Remove(existingParticipant);
            //    }


            //    // Update and Insert participants
            //    foreach (var participant in userMap.Participants)
            //    {
            //        var existingParticipant = existingUser.Participants
            //            .Where(c => c.Id == participant.Id)
            //            .SingleOrDefault();

            //        if (existingParticipant != null)
            //            // Update child
            //            _context.Entry(existingParticipant).CurrentValues.SetValues(participant);
            //        else
            //        {
            //            // Insert child
            //            var newParticipant = new Participant
            //            {
            //                FirstName = participant.FirstName,
            //                LastName = participant.LastName,
            //                MI = participant.MI,
            //                DOB = participant.DOB,
            //                IssuedID = participant.IssuedID,
            //                CompanyID = participant.CompanyID

            //            };
            //            userMap.Participants.Add(newParticipant);
            //        }





            //    }
            //    _context.SaveChanges();
            //}

            //_context.SaveChanges();
            //var xaca = "Stop here";







            // End AM Testing

            // See if the User already exists
            //   var user = _context.Users.Include(x => x.Participants).FirstOrDefault(x => x.Id == dto.User.Id);

            // If the user exists set up for Detachment from the Context
            //var localUser = _context.Set<User>().Local.FirstOrDefault(entry => entry.Id.Equals(dto.User.Id));

            //// If the contact for the user exists set up for Detachment from the Context
            //var localUserContact = _context.Set<Contact>().Local.FirstOrDefault(entry => entry.Id.Equals(dto.User.Id));           

            ////check if localUser is not null
            //if (!localUser.IsNull()) // I'm using an extension method
            //{
            //    // detach
            //    _context.Entry(localUser).State = EntityState.Detached;
            //}

            //// check if localUserContact is not null
            //if (!localUserContact.IsNull())
            //{
            //    // detach
            //    _context.Entry(localUserContact).State = EntityState.Detached;
            //}


            //// Map the DTO to the User Object
            //user = dto.User.Adapt<User>();

            //// Create a new user if the user does not exist
            //if (user.Id == 0)
            //{
            //    // Set Default properties for the User Record
            //    user.Active = 1;
            //    user.DateUpdated = DateTime.Now;
            //    user.DateCreated = DateTime.Now;
            //    user.CreatedBy = dto.CreatedBy;
            //    user.UpdatedBy = dto.UpdatedBy;

            //    // User Contact Record
            //    user.Contact.RecordType = "USR";
            //    user.Contact.DateCreated = DateTime.Now;
            //    user.Contact.DateUpdated = DateTime.Now;
            //    user.Contact.CreatedBy = dto.CreatedBy;
            //    user.Contact.UpdatedBy = dto.UpdatedBy;
            //}

            //// Updated User and Contact
            //else
            //{
            //    // User
            //    user.DateUpdated = DateTime.Now;
            //    user.UpdatedBy = dto.UpdatedBy;

            //    // Contact           
            //    user.Contact.DateUpdated = DateTime.Now;
            //    user.Contact.UpdatedBy = dto.UpdatedBy;
                
            //    // set Modified flag in your entry

            //    _context.Entry(user).State = EntityState.Modified;
            //}

            
            //if (_context.Entry(user).State == EntityState.Modified)
            //{
            //    _context.SaveChanges();
            //}
            //else
            //{
            //    _context.Users.Add(user);
            //    _context.SaveChanges();
            //}


            //   Set the  Default Participant Properites 1 : X
            //user.Participants.ForEach(x =>
            //{
            //    // Check if the participant exists
            //    // If it does, Update the participant
            //    if (x.Id > 0)
            //    {
            //        x.Active = 1;
            //        x.DateUpdated = DateTime.Now;
            //        x.CreatedBy = dto.CreatedBy;
            //        x.UpdatedBy = dto.UpdatedBy;
            //    }
            //    else
            //    {
            //        x.Active = 1;
            //        x.DateUpdated = DateTime.Now;
            //        x.UpdatedBy = dto.UpdatedBy;
            //    }
            //}



            //    // Add Participant Panel 1 : X
            //    x.ParticipantPanels.ForEach(pp =>
            //    {
            //        if (pp.Id > 0)
            //        {
            //            pp.Active = 1;
            //            pp.DateUpdated = DateTime.Now;
            //            pp.ParticipantId = x.Id;
            //            pp.UserId = user.Id;

            //            pp.CreatedBy = dto.CreatedBy;
            //            pp.UpdatedBy = dto.UpdatedBy;

            //        }
            //        else
            //        {
            //            pp.Active = 1;
            //            // pp.DateCreated = DateTime.Now;
            //            pp.DateUpdated = DateTime.Now;

            //            pp.CreatedBy = dto.CreatedBy;
            //            pp.UpdatedBy = dto.UpdatedBy;

            //            // TODO: For testing only
            //            pp.StartDate = DateTime.Now;
            //            pp.EndDate = DateTime.Now.AddMonths(2);
            //        }
            //    });

            //    // Set Participant Schedule 1 : 1
            //    if (x.ParticipantSchedule != null)
            //    {
            //        x.ParticipantSchedule.Active = 1;
            //        x.ParticipantSchedule.DateCreated = DateTime.Now;

            //        x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
            //        x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
            //    }
            //    else
            //    {
            //        x.ParticipantSchedule.DateCreated = DateTime.Now;

            //        x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
            //        x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
            //    }


            //    // Set the Participants Requisitions 1 : X
            //    x.Requisitions.ForEach(r =>
            //    {
            //        r.ReqDate = DateTime.Now.Date;
            //        r.ReqTime = DateTime.Now.TimeOfDay;
            //        r.Active = 1;
            //        r.DateCreated = DateTime.Now;
            //        r.DateUpdated = DateTime.Now;

            //        r.CreatedBy = dto.CreatedBy;
            //        r.UpdatedBy = dto.UpdatedBy;
            //    });

            //    // TODO: set the if condition for if it is a manual test

            //    // Set the Test Schedule if it is a manual test
            //    x.TestSchedules.ForEach(ts =>
            //    {
            //        ts.Active = 1;
            //        ts.DateCreated = DateTime.Now;
            //        ts.DateUpdated = DateTime.Now;
            //        ts.CompanyId = x.CompanyID;
            //        ts.RegionId = x.RegionID;
            //        ts.ParticipantId = x.Id;

            //        ts.CreatedBy = dto.CreatedBy;
            //        ts.UpdatedBy = dto.UpdatedBy;

            //        // Set the Tests Panels if it is a manual test
            //        ts.TestPanels.ForEach(tp =>
            //    {
            //        tp.ScheduleType = 1;
            //        tp.ScheduleModel = "M";
            //        tp.OrderedBy = user.Id;

            //            // Get the panel object by the test panel panel code
            //            var panel = _context.Panels.FirstOrDefault(p => p.LabPanelCode == tp.PanelCode);
            //            // Assign properties from the panel object
            //            tp.PanelID = panel.Id;
            //        tp.LabCode = panel.LabCode;
            //        tp.PanelDescription = panel.Description;
            //    });
            //    });

            //    x.PaternityRelations.ForEach(pr =>
            //    {
            //        pr.Active = 1;
            //        pr.DateCreated = DateTime.Now;
            //        pr.DateUpdated = DateTime.Now;

            //        pr.CreatedBy = dto.CreatedBy;
            //        pr.UpdatedBy = dto.UpdatedBy;
            //    });
            //});




            // Update the Paternity Record with the TestId(td_tests_schedule)        
            //var doesPaternityExist = user.Participants.Any(x => x.PaternityRelations.Any(y => y.Id > 0));

            //if (user.Participants.Any(x => x.PaternityRelations.Any(p => p.Id > 0)))
            //{
            //    user.Participants.ForEach(x => x.PaternityRelations.ForEach(pr =>
            //    {
            //        user.Participants.ForEach(t => t.TestSchedules.ForEach(ts =>
            //       {
            //           pr.TestId = ts.Id;
            //           _context.PaternityRelations.Update(pr);
            //       }));
            //    }));
            //}


            //_context.SaveChanges();


            //var part = user.Participants;


            return null;
        }
    }
}
