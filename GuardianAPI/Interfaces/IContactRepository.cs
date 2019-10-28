using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetContact(int Id);
       Task<IEnumerable<Contact>> GetAllContacts();
        Contact Add(Contact contact);
        Contact UpdateContact(Contact contactChanges);


        // Get Contact by the Record Id of the participant
        Task<Contact> GetContactForParticipantByRecordId(int recordId);
    }
}
