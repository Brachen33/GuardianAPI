using GuardianAPI.Models.PSIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces.PSIManager
{
    public interface IClientRepository
    {
        Client GetClient(int id);
        IEnumerable<Client> GetAll();
        Client GetClientWithUsers(int id);

    }
}
