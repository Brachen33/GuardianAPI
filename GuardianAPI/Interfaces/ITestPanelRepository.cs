using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
    public interface ITestPanelRepository
    {
        IEnumerable<TestPanel> GetAll();
        TestPanel GetById(int id);
    }
}
