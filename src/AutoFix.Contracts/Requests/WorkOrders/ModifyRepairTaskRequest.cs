using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Requests.WorkOrders
{
   public class ModifyRepairTaskRequest
    {
        public Guid[] RepairTaskIds { get; set; } = [];

    }
}
