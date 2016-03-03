using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditLog.Models;

namespace AuditLog.Common
{
    public interface ILogDetailsAuditor
    {
        IEnumerable<AuditLogDetail> CreateLogDetails();
    }
}
