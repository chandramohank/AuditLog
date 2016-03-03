using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AuditLog.Common;
using AuditLog.Models;

namespace AuditLog
{
    public class CoreTracker
    {
        private readonly ITrackerContext _context;

        public CoreTracker(ITrackerContext context)
        {
            _context = context;
        }

        public void AuditChanges(object userName)
        {
            foreach (
                DbEntityEntry ent in
                    _context.ChangeTracker.Entries()
                        .Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                using (var auditer = new LogAuditor(ent))
                {
                    var eventType = GetEventType(ent);

                    Models.AuditLog record = auditer.CreateLogRecord(userName, eventType, _context);
                    if (record != null)
                    {
                        _context.AuditLog.Add(record);
                    }
                }
            }
        }

        public IEnumerable<DbEntityEntry> GetAdditions()
        {
            return _context.ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
        }

        public void AuditAdditions(object userName, IEnumerable<DbEntityEntry> addedEntries)
        {
            // Get all Added entities
            foreach (DbEntityEntry ent in addedEntries)
            {
                using (var auditer = new LogAuditor(ent))
                {
                    Models.AuditLog record = auditer.CreateLogRecord(userName, EventType.Added, _context);
                    if (record != null)
                    {
                        _context.AuditLog.Add(record);
                    }
                }
            }
        }

         private EventType GetEventType(DbEntityEntry entry)
        {
            var eventType = entry.State == EntityState.Modified ? EventType.Modified : EventType.Deleted;
            return eventType;
        }
    }
}
