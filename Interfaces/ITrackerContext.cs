using System.Data.Entity;

namespace AuditLog.Common
{
    public interface ITrackerContext : IDbContext
    {
        DbSet<Models.AuditLog> AuditLog { get; set; }
    }
}
