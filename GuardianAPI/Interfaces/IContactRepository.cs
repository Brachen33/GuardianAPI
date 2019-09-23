using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IContactRepository
    {
        Contact GetContact(int Id);
        IEnumerable<Contact> GetAllContacts();
        Contact Add(Contact contact);
        Contact UpdateContact(Contact contactChanges);
    }
}
