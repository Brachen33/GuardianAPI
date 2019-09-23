using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface IUserRoleRepository
    {
        UserRole GetUserRole(int Id);
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole Add(UserRole userRole);
        UserRole Update(UserRole userRoleChanges);
    }
}
