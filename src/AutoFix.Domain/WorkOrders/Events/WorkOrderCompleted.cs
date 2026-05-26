using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;

namespace AutoFix.Domain.WorkOrders.Events
{
   public sealed class WorkOrderCompleted:DomainEvent
    {
        public Guid WorkOrderId { get; set; }

    }
}
