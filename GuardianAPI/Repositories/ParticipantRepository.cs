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

        public ParticipantRepository(AppDbContext context)
        {
            _context = context;
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
        //        var user = _context.Users.FirstOrDefault(x => x.Id == dto.DTO.Id);

        //    // Create a new user if the user does not exist
        //    if (user == null)
        //    {
        //        var createdUser = dto.DTO.Adapt<User>();               

        //        // Set Default properties for the User Record
        //        createdUser.Active = 1;
        //        createdUser.DateUpdated = DateTime.Now;
        //        createdUser.DateCreated = DateTime.Now;
        //     //   createdUser.Contact.RecordType = "USR";

        //        // Set Default properties for the Participants
        //        //foreach (var participant in createdUser.Participants)
        //        //{
        //        //    participant.DateCreated = DateTime.Now;
        //        //    participant.DateUpdated = DateTime.Now;
        //        //    participant.Active = 1;
        //       //     participant.Contact.RecordType = "PID";
        //    //    }

        //    _context.Users.Add(createdUser);
        //}


        //// TODO: If user does not exist Create the User First
        //if (dto.)
        //{
        //    var user = dto.User.Adapt<User>();
        //    _context.Users.Add(user);                                             
        //}

        //// TODO: Create the Participant
        //var participant = dto.Participant.Adapt<Participant>();
        //_context.Participants.Add(participant);

        //// TODO: Create The Contact
        //var contact = dto.Contact.Adapt<Contact>();
        //_context.Contacts.Add(contact);

        //// TODO: Create the ParticipantSchedule
        //var participantSchedule = dto.ParticipantSchedule.Adapt<ParticipantSchedule>();
        //_context.ParticipantSchedules.Add(participantSchedule);

        //// TODO: Create The Requisition
        //var requistion = dto.Requisition.Adapt<Requisition>();
        //_context.Requisitions.Add(requistion);

        //// TODO: Create the Log Entry
        //var logEntry = dto.LogEntry.Adapt<LogEntry>();
        //_context.LogEntries.Add(logEntry);

        _context.SaveChanges();

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
