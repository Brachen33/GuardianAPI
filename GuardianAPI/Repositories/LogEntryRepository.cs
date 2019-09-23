using GuardianAPI.Interfaces;
using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Repositories
{
    public class LogEntryRepository : ILogEntryRepository
    {
        public LogEntry Add(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogEntry> GetAllLogEntries()
        {
            throw new NotImplementedException();
        }

        public LogEntry GetLogEntry(int Id)
        {
            throw new NotImplementedException();
        }

        public LogEntry Update(LogEntry logEntryChanges)
        {
            throw new NotImplementedException();
        }
    }
}
