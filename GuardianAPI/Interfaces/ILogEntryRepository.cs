using GuardianAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Interfaces
{
   public interface ILogEntryRepository
    {
        LogEntry GetLogEntry(int Id);
        IEnumerable<LogEntry> GetAllLogEntries();
        LogEntry Add(LogEntry logEntry);
        LogEntry Update(LogEntry logEntryChanges);
    }
}
