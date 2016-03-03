using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditLog.ExtentionClasses;

namespace AuditLog.Comparators
{
    internal static class ComparatorFactory
    {
        internal static Comparator GetComparator(Type type)
        {
            if (type.IsNullable<DateTime>())
            {
                return new NullableDateComparator();
            }

            if (type == typeof (DateTime))
            {
                return new DateComparator();
            }

            if (type == typeof (string))
            {
                return new StringComparator();
            }

            if (type.IsNullable())
            {
                return new NullableComparator();
            }

            if (type.IsValueType)
            {
                return new ValueTypeComparator();
            }

            return new Comparator();
        }
    }
}
