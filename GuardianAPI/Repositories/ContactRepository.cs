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
            throw new NotImplementedException();
        }
    }
}
