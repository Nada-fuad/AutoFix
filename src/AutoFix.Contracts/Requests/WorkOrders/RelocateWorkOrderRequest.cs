using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Contracts.Common;

namespace AutoFix.Contracts.Requests.WorkOrders
{
   public class RelocateWorkOrderRequest
    {
        public DateTimeOffset NewStartAtUtc { get; set; }
        public Spot NewSpot { get; set; }

   
    }

}
