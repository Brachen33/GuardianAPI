using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public Contact Add(Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContact(int Id)
        {
            return await _context.Contacts.FindAsync(Id);
        }

        public async Task<Contact> GetContactForParticipantByRecordId(int recordId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.RecordID == recordId && x.RecordType == "PID");

            return contact;
        }

        public Contact UpdateContact(Contact contactChanges)
        {
            var contact = _context.Contacts.Attach(contactChanges);
            contact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return contactChanges;
        }


        
    }
}
