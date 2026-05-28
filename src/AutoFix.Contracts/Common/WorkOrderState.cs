using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Common
{
    public enum WorkOrderState
    {
        Scheduled,
        InProgress,
        Completed,
        Cancelled
    }
}
