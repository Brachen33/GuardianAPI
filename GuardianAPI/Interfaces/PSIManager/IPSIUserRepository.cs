using GuardianAPI.Models.PSIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces.PSIManager
{
    public interface IPSIUserRepository
    {
        PSIUser GetUser(int id);
        IEnumerable<PSIUser> GetAll();
    }
}
