using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Contracts.Common;

namespace AutoFix.Contracts.Requests.WorkOrders
{
   public  class UpdateWorkOrderStateRequest
    {
        public WorkOrderState State { get; set; }

    }
}
