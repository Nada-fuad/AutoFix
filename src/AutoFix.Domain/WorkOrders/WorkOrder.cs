using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.WorkOrders.Enums;

namespace AutoFix.Domain.WorkOrders
{
    public sealed class WorkOrder:AuditableEntity
    {
        public Guid VehicleId { get; private set; }
        public DateTimeOffset StartAtUtc { get; private set; }
        public DateTimeOffset EndAtUtc { get; private set; }
        private List<RepairTask> _repairTasks = [];
        public IReadOnlyCollection<RepairTask> RepairTasks => _repairTasks.AsReadOnly();

        public WorkOrderState State { get; private set; }
        public Vehicle? Vehicle { get; set; }

        private WorkOrder() { }

        private WorkOrder(Guid id, Guid vehicleId, DateTimeOffset startAtUtc, DateTimeOffset endAtUtc,List<RepairTask> repairTasks) :base(id) {
        
        VehicleId = vehicleId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            _repairTasks = repairTasks;


        
        }

        public static Result<WorkOrder> Create(Guid id, Guid vehicleId, DateTimeOffset startAtUtc, DateTimeOffset endAtUtc, List<RepairTask> repairTasks) {

            if(id == Guid.Empty)
            {
                return WorkOrderErrors.WorkOrderIdRequired;
            }

            if (vehicleId == Guid.Empty)
            {
                return WorkOrderErrors.VehicleIdRequired;
            }

            if (repairTasks == null || repairTasks.Count == 0)
            {
                return WorkOrderErrors.RepairTasksRequired;
            }
            if (endAtUtc <= startAtUtc)
            {
                return WorkOrderErrors.InvalidTiming;
            }
        
        return new WorkOrder(id, vehicleId, startAtUtc, endAtUtc, repairTasks);
        }



    }
}
