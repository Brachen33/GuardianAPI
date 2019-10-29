using GuardianAPI.DTOs.Guardian;
using GuardianAPI.Interfaces;
using GuardianAPI.Interfaces.ILoggerManager;
using GuardianAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GuardianAPI.BLL
{
    public class ExternalCreateParticipant : IExternalCreateParticipant
    {
        private readonly AppDbContext _context;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _user;
        private readonly IParticipantRepository _participantRepository;
        private readonly IPanelRepository _panel;
        private readonly IConfiguration _config;
        private readonly IParticipantPanelRepository _participantPanelRepository;



        public ExternalCreateParticipant(AppDbContext context, ILoggerManager logger, IUserRepository user
            , IParticipantRepository participantRepository, IPanelRepository panelRepository,
            IConfiguration config, IParticipantPanelRepository participantPanelRepository)
        {
            _context = context;
            _user = user;
            _participantRepository = participantRepository;
            _panel = panelRepository;
            _logger = logger;
            _config = config;
            _participantPanelRepository = participantPanelRepository;
        }

        public async Task<string> GuardianProcess(GuardianCreateDTO dto)
        {
            // AM Testing This is a test using dapper ()
            var connString = _config.GetConnectionString("DCSConnectionStringDevelopment");
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                var test = connection.Query("Select * from td_users where id = 180");

                // Dapper Contrib
                var testContrib = connection.Get<User>(180);
            }
            // End AM Dapper Testing


            // New Process
            // (Parent)
            //var existingUser = _context.Users
            //    .Where(u => u.Id == dto.User.Id)
            //    .Include(x => x.Contact)
            //    .Include(p => p.Participants)

            //    .SingleOrDefault();

            var existingUser = await _context.Users
                .Where(u => u.Id == dto.User.Id)
                .Include(x => x.Contact)
                .Include(p => p.Participants)
                .Include("Participants.Contact")
                .Include("Participants.ParticipantSchedule")
                .Include("Participants.Requisition")
                .SingleOrDefaultAsync();


            // AM Testing For a new User


            var newUser = new User();
            if (existingUser == null)
            {
                newUser = new User
                {
                    CompanyId = dto.User.CompanyId,
                    RegionId = dto.User.RegionId,
                    CollectionSiteId = dto.User.CollectionSiteId,
                    UserPin = dto.User.UserPin,
                    UserPass = dto.User.UserPass,
                    UserType = "USR",
                    RoleId = dto.User.RoleId,
                    FirstName = dto.User.FirstName,
                    LastName = dto.User.LastName,
                    MI = dto.User.MI,
                    Initials = dto.User.Initials,
                    Ethnicity = dto.User.Ethnicity,
                    DOB = dto.User.DOB,
                    JobTitle = dto.User.JobTitle,
                    SiteCode = dto.User.SiteCode,
                    AppPin = dto.User.AppPin,

                    //Defaults
                    Active = 1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = dto.UserId,
                    UpdatedBy = dto.UserId,

                    //Contact for new User
                    Contact = {
                        RecordType = "USR",
                        Address1 = dto.User.Contact.Address1,
                        City = dto.User.Contact.City,
                        State = dto.User.Contact.State,
                        Zip1 = dto.User.Contact.Zip1,
                        Email = dto.User.Contact.Email,
                        Phone = dto.User.Contact.Phone,

                        // Defaults
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        CreatedBy = dto.UserId,
                        UpdatedBy = dto.UserId
                    }
                };

                _context.Add(newUser);
                _context.SaveChanges();


                // Loop through participants that were in the DTO to see if they exist
                foreach (var participant in dto.User.Participants)
                {
                    // Check if the participant exists, if so load the record with the Contact
                    var existingParticipant = await _context.Participants
                        .Include(x => x.Contact)
                        .FirstOrDefaultAsync(x => x.Id == participant.Id);

                    if (existingParticipant != null)
                    {
                        // Update the Defaults for an existing participant
                        existingParticipant.DateUpdated = DateTime.Now;
                        _context.Entry(existingParticipant).CurrentValues.SetValues(participant);

                        _context.Entry(existingParticipant.Contact).CurrentValues.SetValues(participant.Contact);                       
                    }                    

                    // Create New Participant with a New User
                    else
                    {
                        // check for an existing issued id
                        if (await _participantRepository.DoesParticipantExist(participant.IssuedID))
                        {
                            return "Issued Id already exists.";
                        }


                        // Initialize the new participant panel List
                        List<ParticipantPanel> newParticipantPanels = new List<ParticipantPanel>();

                        foreach (var pPanelDTO in participant.ParticipantPanels)
                        {
                          
                            var pPanel = pPanelDTO.Adapt<ParticipantPanel>();
                            pPanel.Active = 1;
                            pPanel.UserId = dto.UserId;
                            pPanel.CreatedBy = dto.UserId;
                            pPanel.UpdatedBy = dto.UserId;


                            

                            //var pPanel = new ParticipantPanel
                            //{
                            //    CompanyId = pPanelDTO.CompanyId,
                            //    RegionId = pPanelDTO.RegionId,
                            //    UserId = dto.UserId,
                            //    StartDate = pPanelDTO.StartDate,
                            //    EndDate = pPanelDTO.EndDate,
                            //    PanelId = pPanelDTO.PanelId,
                            //    ScheduleType = pPanelDTO.ScheduleType,

                            //    // Set Defaults for a new participant panel
                            //    Active = 1,
                            //    DateCreated = DateTime.Now,
                            //    DateUpdated = DateTime.Now,
                            //    CreatedBy = dto.UserId,
                            //    UpdatedBy = dto.UserId
                            //};

                            // Add The Participant Panel to the initialized list
                            newParticipantPanels.Add(pPanel);
                        }

                        // Initialize the new Test Schedule List
                        var newTestSchedules = new List<TestSchedule>();
                        var newTestPanels = new List<TestPanel>();

                        foreach (var tScheduleDTO in participant.TestSchedules)
                        {                         

                            var tSchedule = new TestSchedule
                            {
                                ParticipantId = participant.Id,
                                CompanyId = tScheduleDTO.CompanyId,
                                RegionId = tScheduleDTO.RegionId,
                                TestDate = tScheduleDTO.TestDate,
                                TestTime = tScheduleDTO.TestTime,
                                ScheduleType = tScheduleDTO.ScheduleType,

                                // set the defaults for the test schedule
                                Active = 1,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId                        
                            };

                            // Add the Test Panel List here                        
                            foreach (var tPanel in tScheduleDTO.TestPanels)
                            {
                                // Get the Panel to populate fields
                                var panel = await _panel.GetPanel(tPanel.PanelID);

                                var newTPanel = new TestPanel
                                {
                                    PanelID = tPanel.PanelID,
                                    PanelDescription = panel.Description,
                                    LabCode = panel.LabCode,                                   
                                    PanelCode = panel.LabPanelCode,
                                    ScheduleType = tPanel.ScheduleType,
                                    ScheduleModel = tPanel.ScheduleModel,
                                    CompanyID = tScheduleDTO.CompanyId,
                                    RegionID = tScheduleDTO.RegionId
                                };
                                newTestPanels.Add(newTPanel);
                            }

                            tSchedule.TestPanels.AddRange(newTestPanels);

                            newTestSchedules.Add(tSchedule);
                        }
                       
                        _context.TestSchedules.AddRange(newTestSchedules);


                       
                        var newParticipant = new Participant
                        {
                            CompanyID = participant.CompanyID,
                            FirstName = participant.FirstName,
                            LastName = participant.LastName,
                            IssuedID = participant.IssuedID,
                            MI = participant.MI,
                            DOB = participant.DOB,
                            Gender = participant.Gender,
                            StartDate = participant.StartDate,
                            EndDate = participant.EndDate,

                            Active = 1,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                            CreatedBy = dto.UserId,
                            UpdatedBy = dto.UserId,

                            // Add or Update the Contact record
                            Contact = new Contact
                            {
                                RecordType = "PID",
                                Address1 = participant.Contact.Address1,
                                City = participant.Contact.City,
                                State = participant.Contact.State,
                                Zip1 = participant.Contact.Zip1,
                                Phone = participant.Contact.Phone,
                                Email = participant.Contact.Email,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },
                            ParticipantSchedule = new ParticipantSchedule
                            {
                                ParticipantId = participant.Id,
                                StartDate = participant.ParticipantSchedule.StartDate,
                                EndDate = participant.ParticipantSchedule.EndDate,
                                ScheduleId = participant.ParticipantSchedule.ScheduleId,
                                ScheduleModel = participant.ParticipantSchedule.ScheduleModel,
                                Frequency = participant.ParticipantSchedule.Frequency,
                                Sunday = participant.ParticipantSchedule.Sunday,
                                Monday = participant.ParticipantSchedule.Monday,
                                Tuesday = participant.ParticipantSchedule.Tuesday,
                                Wednesday = participant.ParticipantSchedule.Wednesday,
                                Thursday = participant.ParticipantSchedule.Thursday,
                                Friday = participant.ParticipantSchedule.Friday,
                                Saturday = participant.ParticipantSchedule.Saturday,

                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },
                            Requisition = new Requisition
                            {
                                CompanyId = participant.Requisition.CompanyId,
                                RegionId = participant.Requisition.RegionId,
                                ParticipantIssuedId = participant.IssuedID,
                                ParticipantFName = participant.FirstName,
                                ParticipantLName = participant.LastName,
                                RecordType = "Requisition",
                                CaseNumber = participant.Requisition.CaseNumber,
                                ProfileCode = 0,
                                // TODO: take a look at other tables to see if you can populate schedule id
                                ProfileDescription = participant.Requisition.ProfileDescription,
                                ScheduleId = participant.Requisition.ScheduleId,
                                ScheduleModel = participant.Requisition.ScheduleModel,
                                ScheduleFreq = participant.Requisition.ScheduleFreq,
                                ScheduleSunday = participant.Requisition.ScheduleSunday,
                                ScheduleMonday = participant.Requisition.ScheduleMonday,
                                ScheduleTuesday = participant.Requisition.ScheduleTuesday,
                                ScheduleWednesday = participant.Requisition.ScheduleWednesday,
                                ScheduleThursday = participant.Requisition.ScheduleThursday,
                                ScheduleFriday = participant.Requisition.ScheduleFriday,
                                ScheduleSaturday = participant.Requisition.ScheduleSaturday,
                                StartDate = participant.Requisition.StartDate,
                                EndDate = participant.Requisition.EndDate,
                                CaseManagerId = newUser.Id,
                                CaseManagerFName = dto.User.FirstName,
                                CaseManagerLName = dto.User.LastName,
                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },
                            ParticipantPanels = newParticipantPanels
                        };

                        _context.Participants.Add(newParticipant);
                        }
                }             
            }
            // End New User AM Testing

            // Process for an existing User Adding or updating Participant and Participant Profiles
            if (existingUser != null)
            {

                //Update User(Parent)
                dto.User.DateUpdated = DateTime.Now;
                _context.Entry(existingUser).CurrentValues.SetValues(dto.User);

                // Update and Insert Participants
                foreach (var participant in dto.User.Participants)
                {
                    var existingParticipant = existingUser.Participants                                    
                        .Where(c => c.Id == participant.Id)                        
                        .SingleOrDefault();

                    if (existingParticipant != null)
                    {
                        // Update participant                       
                        _context.Entry(existingParticipant).CurrentValues.SetValues(participant);

                        //If Participant Contact exists Update Participant Contact
                        if (existingParticipant.Contact.Id != 0)
                        {
                            // Get the Existing Contact
                            var existingContact = existingParticipant.Contact;

                            _context.Entry(existingParticipant.Contact).CurrentValues.SetValues(existingContact);
                        }
                        // This is a new Contact on an existing participant
                        else
                        {
                            var newContact = new Contact
                            {
                                RecordID = participant.Id,
                                RecordType = "PID",
                                Address1 = participant.Contact.Address1,
                                City = participant.Contact.City,
                                State = participant.Contact.State,
                                Zip1 = participant.Contact.Zip1,
                                Phone = participant.Contact.Phone,
                                Email = participant.Contact.Email,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            };
                            _context.Contacts.Add(newContact);
                        }
                        // Participant Schedule ALWAYS gets a new insert
                        if (participant.ParticipantSchedule != null)
                        {
                            var participantSchedule = new ParticipantSchedule
                            {
                                ParticipantId = participant.Id,
                                StartDate = participant.ParticipantSchedule.StartDate,
                                EndDate = participant.ParticipantSchedule.EndDate,
                                ScheduleId = participant.ParticipantSchedule.ScheduleId,
                                ScheduleModel = participant.ParticipantSchedule.ScheduleModel,
                                Frequency = participant.ParticipantSchedule.Frequency,
                                Sunday = participant.ParticipantSchedule.Sunday,
                                Monday = participant.ParticipantSchedule.Monday,
                                Tuesday = participant.ParticipantSchedule.Tuesday,
                                Wednesday = participant.ParticipantSchedule.Wednesday,
                                Thursday = participant.ParticipantSchedule.Thursday,
                                Friday = participant.ParticipantSchedule.Friday,
                                Saturday = participant.ParticipantSchedule.Saturday,

                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            };
                            _context.ParticipantSchedules.Add(participantSchedule);
                        }


                        // If a requisition exists Add it
                        if (participant.Requisition != null)
                        {
                            var requisition = new Requisition
                            {
                                CompanyId = participant.Requisition.CompanyId,
                                RegionId = participant.Requisition.RegionId,
                                ParticipantIssuedId = participant.IssuedID,
                                ParticipantFName = participant.FirstName,
                                ParticipantLName = participant.LastName,
                                RecordType = "Requisition",
                                CaseNumber = participant.Requisition.CaseNumber,
                                ProfileCode = 0,
                                // TODO: take a look at other tables to see if you can populate schedule id
                                ProfileDescription = participant.Requisition.ProfileDescription,
                                ScheduleId = participant.Requisition.ScheduleId,
                                ScheduleModel = participant.Requisition.ScheduleModel,
                                ScheduleFreq = participant.Requisition.ScheduleFreq,
                                ScheduleSunday = participant.Requisition.ScheduleSunday,
                                ScheduleMonday = participant.Requisition.ScheduleMonday,
                                ScheduleTuesday = participant.Requisition.ScheduleTuesday,
                                ScheduleWednesday = participant.Requisition.ScheduleWednesday,
                                ScheduleThursday = participant.Requisition.ScheduleThursday,
                                ScheduleFriday = participant.Requisition.ScheduleFriday,
                                ScheduleSaturday = participant.Requisition.ScheduleSaturday,
                                StartDate = participant.Requisition.StartDate,
                                EndDate = participant.Requisition.EndDate,
                                CaseManagerId = existingUser.Id,
                                CaseManagerFName = dto.User.FirstName,
                                CaseManagerLName = dto.User.LastName,
                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            };
                            _context.Requisitions.Add(requisition);
                        }

                        // TODO: Check if this is validating correctly
                        if (participant.ParticipantPanels.Count > 0)
                        {
                            var pPanels = new List<ParticipantPanel>();

                            foreach (var pPanel in participant.ParticipantPanels)
                            {
                                var participantPanel = new ParticipantPanel
                                {
                                    CompanyId = participant.CompanyID,
                                    RegionId = participant.RegionID,
                                    UserId = existingUser.Id,
                                    ParticipantId = participant.Id,
                                    PanelId = pPanel.PanelId,
                                    ScheduleId = pPanel.ScheduleId,
                                    ScheduleType = pPanel.ScheduleType,
                                    StartDate = pPanel.StartDate,
                                    EndDate = pPanel.EndDate,

                                    //Defaults
                                    Active = 1,
                                    DateCreated = DateTime.Now,
                                    DateUpdated = DateTime.Now,
                                    CreatedBy = dto.UserId,
                                    UpdatedBy = dto.UserId
                                };
                                pPanels.Add(participantPanel);
                            }
                            _context.ParticipantPanels.AddRange(pPanels);
                        }
                    }


                    // If the Participant does not exist, create the new records
                    else
                    {
                        // check for an existing issued id
                        if (await _participantRepository.DoesParticipantExist(participant.IssuedID))
                        {
                            return "Issued Id already exists.";
                        }

                        // Insert Participant
                        var newParticipant = new Participant
                        {
                            CompanyID = participant.CompanyID,
                            FirstName = participant.FirstName,
                            LastName = participant.LastName,
                            IssuedID = participant.IssuedID,
                            MI = participant.MI,
                            DOB = participant.DOB,
                            Gender = participant.Gender,
                            StartDate = participant.StartDate,
                            EndDate = participant.EndDate,

                            Active = 1,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                            CreatedBy = dto.UserId,
                            UpdatedBy = dto.UserId,

                            // Add or Update the Contact record
                            Contact = new Contact
                            {
                                RecordType = "PID",
                                Address1 = participant.Contact.Address1,
                                City = participant.Contact.City,
                                State = participant.Contact.State,
                                Zip1 = participant.Contact.Zip1,
                                Phone = participant.Contact.Phone,
                                Email = participant.Contact.Email,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },

                            // Participant Schedule Create (NEVER UPDATED)
                            ParticipantSchedule = new ParticipantSchedule
                            {
                                StartDate = participant.ParticipantSchedule.StartDate,
                                EndDate = participant.ParticipantSchedule.EndDate,
                                ScheduleId = participant.ParticipantSchedule.ScheduleId,
                                ScheduleModel = participant.ParticipantSchedule.ScheduleModel,
                                Frequency = participant.ParticipantSchedule.Frequency,
                                Sunday = participant.ParticipantSchedule.Sunday,
                                Monday = participant.ParticipantSchedule.Monday,
                                Tuesday = participant.ParticipantSchedule.Tuesday,
                                Wednesday = participant.ParticipantSchedule.Wednesday,
                                Thursday = participant.ParticipantSchedule.Thursday,
                                Friday = participant.ParticipantSchedule.Friday,
                                Saturday = participant.ParticipantSchedule.Saturday,

                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },
                            Requisition = new Requisition
                            {
                                CompanyId = participant.Requisition.CompanyId,
                                RegionId = participant.Requisition.RegionId,
                                ParticipantIssuedId = participant.IssuedID,
                                ParticipantFName = participant.FirstName,
                                ParticipantLName = participant.LastName,
                                RecordType = "Requisition",
                                CaseNumber = participant.Requisition.CaseNumber,
                                ProfileCode = 0,
                                // TODO: take a look at other tables to see if you can populate schedule id
                                ProfileDescription = participant.Requisition.ProfileDescription,
                                ScheduleId = participant.Requisition.ScheduleId,
                                ScheduleModel = participant.Requisition.ScheduleModel,
                                ScheduleFreq = participant.Requisition.ScheduleFreq,
                                ScheduleSunday = participant.Requisition.ScheduleSunday,
                                ScheduleMonday = participant.Requisition.ScheduleMonday,
                                ScheduleTuesday = participant.Requisition.ScheduleTuesday,
                                ScheduleWednesday = participant.Requisition.ScheduleWednesday,
                                ScheduleThursday = participant.Requisition.ScheduleThursday,
                                ScheduleFriday = participant.Requisition.ScheduleFriday,
                                ScheduleSaturday = participant.Requisition.ScheduleSaturday,
                                StartDate = participant.Requisition.StartDate,
                                EndDate = participant.Requisition.EndDate,
                                CaseManagerId = existingUser.Id,
                                CaseManagerFName = dto.User.FirstName,
                                CaseManagerLName = dto.User.LastName,

                                // Defaults
                                Active = 1,
                                DateCreated = DateTime.Now,
                                DateUpdated = DateTime.Now,
                                CreatedBy = dto.UserId,
                                UpdatedBy = dto.UserId
                            },

                            ParticipantPanels = participant.ParticipantPanels.Adapt<List<ParticipantPanel>>(),


                        };
                        existingUser.Participants.Add(newParticipant);
                    }
                }
                _context.SaveChanges();
                return "Success";
            }

            _context.SaveChanges();
            return "Success";

            // End New Proces











            //var userMapped = dto.User.Adapt<User>();

            //// Check If the user exists
            //var user = _context.Users
            //     .Where(u => u.Id == dto.User.Id)
            //     .Include(x => x.Contact)
            //     .Include(p => p.Participants)
            //     .SingleOrDefault();

            //// check if existing user exists
            //if (user == null)
            //{
            //    if (userMapped.Contact == null)
            //    {
            //        return "A User must contain contact information";
            //    }

            //    userMapped.DateCreated = DateTime.Now;
            //    userMapped.DateUpdated = DateTime.Now;
            //    userMapped.Active = 1;
            //    userMapped.CreatedBy = dto.UserId;
            //    userMapped.UpdatedBy = dto.UserId;
            //    userMapped.Contact.DateCreated = DateTime.Now;
            //    userMapped.Contact.DateUpdated = DateTime.Now;
            //    userMapped.Contact.CreatedBy = dto.UserId;
            //    userMapped.Contact.UpdatedBy = dto.UserId;

            //    // Check if participants were added as well if not save just the User and there Contact
            //    if (userMapped.Participants.Count > 0)
            //    {
            //        foreach (var participant in userMapped.Participants)
            //        {
            //            if (_participantRepository.DoesParticipantExist(participant.IssuedID))
            //            {
            //                return $"Participant with Issued Id {participant.IssuedID} already exists";
            //            }

            //            // Add the participant contact record
            //            if (participant.Contact == null)
            //            {
            //                return $"Participant {participant.FirstName} {participant.LastName} must have a contact record";
            //            }

            //            // Add the contact defaults
            //            participant.Contact.CreatedBy = dto.UserId;
            //            participant.Contact.UpdatedBy = dto.UserId;
            //            participant.Contact.DateCreated = DateTime.Now;
            //            participant.Contact.DateUpdated = DateTime.Now;

            //            // Add the participant Schedule Defaults
            //            participant.ParticipantSchedule.DateCreated = DateTime.Now;
            //            participant.ParticipantSchedule.Active = 1;
            //            participant.ParticipantSchedule.CreatedBy = dto.UserId;
            //            participant.ParticipantSchedule.UpdatedBy = dto.UserId;

            //            // Set participant panel Defaults
            //            foreach (var participantPanel in participant.ParticipantPanels)
            //            {
            //                participantPanel.DateCreated = DateTime.Now;
            //                participantPanel.DateUpdated = DateTime.Now;
            //                participantPanel.Active = 1;
            //                participantPanel.CreatedBy = dto.UserId;
            //                participantPanel.UpdatedBy = dto.UserId;
            //            }

            //            // Set the Participant Requisition Defaults
            //            foreach (var req in participant.Requisitions)
            //            {
            //                req.DateCreated = DateTime.Now;
            //                req.DateUpdated = DateTime.Now;
            //                req.Active = 1;
            //                req.CreatedBy = dto.UserId;
            //                req.UpdatedBy = dto.UserId;
            //                req.ReqDate = DateTime.Now.Date;
            //                req.ReqTime = DateTime.Now.TimeOfDay;
            //            }

            //            // Set the TestSchedule Defaults
            //            foreach (var testSchedule in participant.TestSchedules)
            //            {
            //                testSchedule.DateCreated = DateTime.Now;
            //                testSchedule.DateUpdated = DateTime.Now;
            //                testSchedule.Active = 1;
            //                testSchedule.CreatedBy = dto.UserId;
            //                testSchedule.UpdatedBy = dto.UserId;

            //                // Set the test panel records
            //                foreach (var tp in testSchedule.TestPanels)
            //                {
            //                    // Get the panel by panel code
            //                    var panel = _panel.GetPanelByPanelCode(tp.PanelCode);

            //                    tp.PanelID = panel.Id;
            //                    tp.LabCode = panel.LabCode;
            //                    tp.PanelDescription = panel.Description;
            //                    tp.OrderedBy = dto.UserId;
            //                }
            //            }

            //            if (participant.PaternityRelations != null)
            //            {
            //                foreach (var paternity in participant.PaternityRelations)
            //                {
            //                    paternity.DateCreated = DateTime.Now;
            //                    paternity.DateUpdated = DateTime.Now;
            //                    paternity.Active = 1;
            //                    paternity.CreatedBy = dto.UserId;
            //                    paternity.UpdatedBy = dto.UserId;
            //                }
            //            }

            //            // Add the participant defaults
            //            participant.Active = 1;
            //            participant.CreatedBy = dto.UserId;
            //            participant.UpdatedBy = dto.UserId;
            //            participant.DateCreated = DateTime.Now;
            //            participant.DateUpdated = DateTime.Now;
            //        };

            //        // Add the User Defaults

            //        userMapped.DateCreated = DateTime.Now;
            //        userMapped.DateUpdated = DateTime.Now;
            //        userMapped.Active = 1;
            //        userMapped.CreatedBy = dto.UserId;
            //        userMapped.UpdatedBy = dto.UserId;
            //        userMapped.Contact.DateCreated = DateTime.Now;
            //        userMapped.Contact.DateUpdated = DateTime.Now;
            //        userMapped.Contact.CreatedBy = dto.UserId;
            //        userMapped.Contact.UpdatedBy = dto.UserId;

            //        _context.Add(userMapped);
            //        _context.SaveChanges();
            //    }

            //    return $"Success!";
            //}
            //// Update User only
            //else if(user != null)
            //{

            //}



            //_context.Add(user);
            //_context.SaveChanges();

            //return "Success!";











            // See if the User already exists
            //       var user = _context.Users.Include(x => x.Participants).FirstOrDefault(x => x.Id == dto.User.Id);

            //    If the user exists set up for Detachment from the Context

            //      var localUser = _context.Set<User>().Local.FirstOrDefault(entry => entry.Id.Equals(dto.User.Id));

            //       // If the contact for the user exists set up for Detachment from the Context
            //       var localUserContact = _context.Set<Contact>().Local.FirstOrDefault(entry => entry.Id.Equals(dto.User.Id));

            //       //check if localUser is not null
            //       if (!localUser.IsNull()) // I'm using an extension method
            //       {
            //           // detach
            //           _context.Entry(localUser).State = EntityState.Detached;
            //       }

            //       // check if localUserContact is not null
            //       if (!localUserContact.IsNull())
            //       {
            //           // detach
            //           _context.Entry(localUserContact).State = EntityState.Detached;
            //       }


            //       // Map the DTO to the User Object
            //       user = dto.User.Adapt<User>();

            //       // Create a new user if the user does not exist
            //       if (user.Id == 0)
            //       {
            //           // Set Default properties for the User Record
            //           user.Active = 1;
            //           user.DateUpdated = DateTime.Now;
            //           user.DateCreated = DateTime.Now;
            //           user.CreatedBy = dto.CreatedBy;
            //           user.UpdatedBy = dto.UpdatedBy;

            //           // User Contact Record
            //           user.Contact.RecordType = "USR";
            //           user.Contact.DateCreated = DateTime.Now;
            //           user.Contact.DateUpdated = DateTime.Now;
            //           user.Contact.CreatedBy = dto.CreatedBy;
            //           user.Contact.UpdatedBy = dto.UpdatedBy;
            //       }

            //       // Updated User and Contact
            //       else
            //       {
            //           // User
            //           user.DateUpdated = DateTime.Now;
            //           user.UpdatedBy = dto.UpdatedBy;

            //           // Contact           
            //           user.Contact.DateUpdated = DateTime.Now;
            //           user.Contact.UpdatedBy = dto.UpdatedBy;

            //           // set Modified flag in your entry

            //           _context.Entry(user).State = EntityState.Modified;
            //       }


            //       if (_context.Entry(user).State == EntityState.Modified)
            //       {
            //           _context.SaveChanges();
            //       }
            //       else
            //       {
            //           _context.Users.Add(user);
            //           _context.SaveChanges();
            //       }


            ////       Set the  Default Participant Properites 1 : X
            //       user.Participants.ForEach(x =>
            //       {
            //           // Check if the participant exists
            //           // If it does, Update the participant
            //           if (x.Id > 0)
            //           {
            //               x.Active = 1;
            //               x.DateUpdated = DateTime.Now;
            //               x.CreatedBy = dto.CreatedBy;
            //               x.UpdatedBy = dto.UpdatedBy;
            //           }
            //           else
            //           {
            //               x.Active = 1;
            //               x.DateUpdated = DateTime.Now;
            //               x.UpdatedBy = dto.UpdatedBy;
            //           }
            //       }



            //           // Add Participant Panel 1 : X
            //           x.ParticipantPanels.ForEach(pp =>
            //           {
            //               if (pp.Id > 0)
            //               {
            //                   pp.Active = 1;
            //                   pp.DateUpdated = DateTime.Now;
            //                   pp.ParticipantId = x.Id;
            //                   pp.UserId = user.Id;

            //                   pp.CreatedBy = dto.CreatedBy;
            //                   pp.UpdatedBy = dto.UpdatedBy;

            //               }
            //               else
            //               {
            //                   pp.Active = 1;
            //                   // pp.DateCreated = DateTime.Now;
            //                   pp.DateUpdated = DateTime.Now;

            //                   pp.CreatedBy = dto.CreatedBy;
            //                   pp.UpdatedBy = dto.UpdatedBy;

            //                   // TODO: For testing only
            //                   pp.StartDate = DateTime.Now;
            //                   pp.EndDate = DateTime.Now.AddMonths(2);
            //               }
            //           });

            //       // Set Participant Schedule 1 : 1
            //       if (x.ParticipantSchedule != null)
            //       {
            //           x.ParticipantSchedule.Active = 1;
            //           x.ParticipantSchedule.DateCreated = DateTime.Now;

            //           x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
            //           x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
            //       }
            //       else
            //       {
            //           x.ParticipantSchedule.DateCreated = DateTime.Now;

            //           x.ParticipantSchedule.CreatedBy = dto.CreatedBy;
            //           x.ParticipantSchedule.UpdatedBy = dto.UpdatedBy;
            //       }


            //       // Set the Participants Requisitions 1 : X
            //       x.Requisitions.ForEach(r =>
            //       {
            //           r.ReqDate = DateTime.Now.Date;
            //           r.ReqTime = DateTime.Now.TimeOfDay;
            //           r.Active = 1;
            //           r.DateCreated = DateTime.Now;
            //           r.DateUpdated = DateTime.Now;

            //           r.CreatedBy = dto.CreatedBy;
            //           r.UpdatedBy = dto.UpdatedBy;
            //       });

            //       // TODO: set the if condition for if it is a manual test

            //       // Set the Test Schedule if it is a manual test
            //       x.TestSchedules.ForEach(ts =>
            //       {
            //           ts.Active = 1;
            //           ts.DateCreated = DateTime.Now;
            //           ts.DateUpdated = DateTime.Now;
            //           ts.CompanyId = x.CompanyID;
            //           ts.RegionId = x.RegionID;
            //           ts.ParticipantId = x.Id;

            //           ts.CreatedBy = dto.CreatedBy;
            //           ts.UpdatedBy = dto.UpdatedBy;

            //               // Set the Tests Panels if it is a manual test
            //               ts.TestPanels.ForEach(tp =>
            //       {
            //           tp.ScheduleType = 1;
            //           tp.ScheduleModel = "M";
            //           tp.OrderedBy = user.Id;

            //               // Get the panel object by the test panel panel code
            //               var panel = _context.Panels.FirstOrDefault(p => p.LabPanelCode == tp.PanelCode);
            //               // Assign properties from the panel object
            //               tp.PanelID = panel.Id;
            //           tp.LabCode = panel.LabCode;
            //           tp.PanelDescription = panel.Description;
            //       });
            //       });

            //       x.PaternityRelations.ForEach(pr =>
            //       {
            //           pr.Active = 1;
            //           pr.DateCreated = DateTime.Now;
            //           pr.DateUpdated = DateTime.Now;

            //           pr.CreatedBy = dto.CreatedBy;
            //           pr.UpdatedBy = dto.UpdatedBy;
            //       });
            //   });




            //        Update the Paternity Record with the TestId(td_tests_schedule)
            //       var doesPaternityExist = user.Participants.Any(x => x.PaternityRelations.Any(y => y.Id > 0));

            //       if (user.Participants.Any(x => x.PaternityRelations.Any(p => p.Id > 0)))
            //       {
            //           user.Participants.ForEach(x => x.PaternityRelations.ForEach(pr =>
            //           {
            //               user.Participants.ForEach(t => t.TestSchedules.ForEach(ts =>
            //              {
            //                  pr.TestId = ts.Id;
            //                  _context.PaternityRelations.Update(pr);
            //              }));
            //           }));
            //       }


            //       _context.SaveChanges();


            //       var part = user.Participants;



        }




    }
}
