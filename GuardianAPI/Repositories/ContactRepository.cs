using GuardianAPI.Interfaces;
using GuardianAPI.Models;
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

        public IEnumerable<Contact> GetAllContacts()
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(int Id)
        {
            throw new NotImplementedException();
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
