using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Domain.WorkOrders
{
    public static class WorkOrderErrors
    {

        public static Error VehicleIdRequired => Error.Validation(code: "WorkOrderErrors.VehicleIdRequired",
        message: "Vehicle Id is required");


        public static Error WorkOrderIdRequired => Error.Validation(code: "WorkOrderErrors.WorkOrderIdRequired", message: "WorkOrder Id is required");


        public static Error RepairTasksRequired => Error.Validation(code: "WorkOrderErrors.RepairTasksRequired", message: "RepairTasks is required");


       public static Error InvalidTiming => Error.Conflict(
        code: "WorkOrderErrors.InvalidTiming",
        message: "End time must be after start time.");
    }
}
