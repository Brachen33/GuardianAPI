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

namespace GuardianAPI.BLL
{
    public class ExternalCreateParticipant : IExternalCreateParticipant
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;

        public ExternalCreateParticipant(AppDbContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public User CreateParticipant(GuardianCreateDTO dto)
        {
            // TODO: Check if the user exists

            var user = _context.Users.Include(x => x.Participants).FirstOrDefault(x => x.Id == dto.User.Id);           

            // Map the DTO to the User Object
            user = dto.User.Adapt<User>();

            // Create a new user if the user does not exist
            if (user.Id == 0)
            {
                // Set Default properties for the User Record
                user.Active = 1;
                user.DateUpdated = DateTime.Now;
                user.DateCreated = DateTime.Now;

                user.CreatedBy = dto.CreatedBy;
                user.UpdatedBy = dto.UpdatedBy;

                // User Contact Record
                user.Contact.RecordType = "USR";
                user.Contact.DateCreated = DateTime.Now;
                user.Contact.DateUpdated = DateTime.Now;

                user.Contact.CreatedBy = dto.CreatedBy;
                user.Contact.UpdatedBy = dto.UpdatedBy;
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

                    x.CreatedBy = dto.CreatedBy;
                    x.UpdatedBy = dto.UpdatedBy;
                }
                else
                {
                    x.Active = 1;
                    x.DateCreated = DateTime.Now;
                    x.DateUpdated = DateTime.Now;

                    x.CreatedBy = dto.CreatedBy;
                    x.UpdatedBy = dto.UpdatedBy;
                }
                // TODO: Figure out Contact
                x.Contact.DateCreated = DateTime.Now;
                x.Contact.DateUpdated = DateTime.Now;
                x.Contact.RecordType = "PID";
                x.Contact.RecordID = x.Id;

                x.Contact.CreatedBy = dto.CreatedBy;
                x.Contact.UpdatedBy = dto.UpdatedBy;

                // Add Participant Panel 1 : X
                x.ParticipantPanels.ForEach(pp =>
                {
                    if (pp.Id > 0)
                    {
                        pp.Active = 1;
                        pp.DateUpdated = DateTime.Now;
                        pp.ParticipantId = x.Id;
                        pp.UserId = user.Id;

                        pp.CreatedBy = dto.CreatedBy;
                        pp.UpdatedBy = dto.UpdatedBy;

                    }
                    else
                    {
                        pp.Active = 1;
                        // pp.DateCreated = DateTime.Now;
                        pp.DateUpdated = DateTime.Now;

                        pp.CreatedBy = dto.CreatedBy;
                        pp.UpdatedBy = dto.UpdatedBy;

                        // TODO: For testing only
                        pp.StartDate = DateTime.Now;
                        pp.EndDate = DateTime.Now.AddMonths(2);
                    }
                });

                // Set Participant Schedule 1 : 1
                if (x.ParticipantSchedule != null)
                {
                    x.ParticipantSchedule.Active = 1;
                    x.ParticipantSchedule.DateCreated = DateTime.Now;

                    x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
                    x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
                }
                else
                {
                    x.ParticipantSchedule.DateCreated = DateTime.Now;

                    x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
                    x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
                }


                // Set the Participants Requisitions 1 : X
                x.Requisitions.ForEach(r =>
                {
                    r.ReqDate = DateTime.Now.Date;
                    r.ReqTime = DateTime.Now.TimeOfDay;
                    r.Active = 1;
                    r.DateCreated = DateTime.Now;
                    r.DateUpdated = DateTime.Now;

                    r.CreatedBy = dto.CreatedBy;
                    r.UpdatedBy = dto.UpdatedBy;
                });

                // TODO: set the if condition for if it is a manual test
         
                    // Set the Test Schedule if it is a manual test
                    x.TestSchedules.ForEach(ts =>
                    {
                        ts.Active = 1;
                        ts.DateCreated = DateTime.Now;
                        ts.DateUpdated = DateTime.Now;
                        ts.CompanyId = x.CompanyID;
                        ts.RegionId = x.RegionID;
                        ts.ParticipantId = x.Id;

                        ts.CreatedBy = dto.CreatedBy;
                        ts.UpdatedBy = dto.UpdatedBy;

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

                x.PaternityRelations.ForEach(pr =>
                {
                    pr.Active = 1;
                    pr.DateCreated = DateTime.Now;
                    pr.DateUpdated = DateTime.Now;

                    pr.CreatedBy = dto.CreatedBy;
                    pr.UpdatedBy = dto.UpdatedBy;
                });                
            });


            // Save original Context
            _context.Users.Add(user);
            var savedContext = _context.SaveChanges();
          
            // Update Context for CreatedBy and UpdatedBy


            

            return user;
        }
    }
}
