using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int Id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> Add(User user);
        Task<User> Update(User userChanges);

        Task<User> GetUserWithParticipantsByIdAsync(int id);
    }
}
