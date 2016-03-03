using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditLog.FactoryClasses;
using AuditLog.Models;

namespace AuditLog.Common
{
    internal class LogAuditor : IDisposable
    {
        private readonly DbEntityEntry _dbEntry;

        internal LogAuditor(DbEntityEntry dbEntry)
        {
            _dbEntry = dbEntry;
        }

        public void Dispose()
        {
        }

        internal Models.AuditLog CreateLogRecord(object userName, EventType eventType, ITrackerContext context)
        {
            var newlog = new Models.AuditLog();
            {

            }
            var detailsAuditor = GetDetailsAuditor(eventType, newlog);
            var Records = detailsAuditor.CreateLogDetails();
            return newlog;
        }

        private ChangeLogDetailsAuditor GetDetailsAuditor(EventType eventType, Models.AuditLog newlog)
        {
            switch (eventType)
            {
                case EventType.Added:
                    return new AdditionLogDetailsAuditor(_dbEntry, newlog);

                case EventType.Deleted:
                    return new DeletetionLogDetailsAuditor(_dbEntry, newlog);

                case EventType.Modified:
                    return new ChangeLogDetailsAuditor(_dbEntry, newlog);

                default:
                    return null;
            }
        }
    }
}
