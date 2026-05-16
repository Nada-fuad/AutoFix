using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.Employees;
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
        public Guid? LaborId { get; private set; }

        public Employee? Labor { get; set; }

        public Spot spot { get; set; }
        private WorkOrder() { }

        private WorkOrder(Guid id, Guid vehicleId, DateTimeOffset startAtUtc, DateTimeOffset endAtUtc,List<RepairTask> repairTasks, Guid? laborId) :base(id) {
        
        VehicleId = vehicleId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            _repairTasks = repairTasks;
            LaborId = laborId;


        
        }

        public static Result<WorkOrder> Create(Guid id, Guid vehicleId, DateTimeOffset startAtUtc, DateTimeOffset endAtUtc, List<RepairTask> repairTasks, Guid? laborId) {

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
            if (laborId == Guid.Empty)
            {
                return WorkOrderErrors.LaborIdRequired;
            }

            return new WorkOrder(id, vehicleId, startAtUtc, endAtUtc, repairTasks, laborId);
        }


        public Result<Updated> AddRepairTask(RepairTask repairTask)
        {
            if (!IsEditable)
                return WorkOrderErrors.Readonly;

            if (_repairTasks.Any(r => r.Id == repairTask.Id))
            {
                return WorkOrderErrors.RepairTaskAlreadyAdded;
            }

            _repairTasks.Add(repairTask);
            return Result.Updated;
        }

        public Result<Updated> ClearRepairTasks()
        {
            if (!IsEditable)
            {
                return WorkOrderErrors.Readonly;
            }

            _repairTasks.Clear();

            return Result.Updated;
        }
        public bool IsEditable => State is not (WorkOrderState.Completed or WorkOrderState.Cancelled or WorkOrderState.InProgress);

        public Result<Updated> UpdateLabor(Guid laborId)
        {
            if (!IsEditable)
            {
                return WorkOrderErrors.Readonly;
            }
            if (laborId == Guid.Empty)
            {
                return WorkOrderErrors.LaborIdEmpty(Id.ToString());
            }

           LaborId = laborId;

            return Result.Updated;
        }



        public Result<Updated> UpdateSpot(Spot newSpot)
        {
            if (IsEditable)
            {
                return WorkOrderErrors.Readonly;
            }

            if (!Enum.IsDefined(newSpot))
            {
                return WorkOrderErrors.SpotInvalid;
            }

            spot = newSpot;

            return Result.Updated;
        }


        public Result<Updated> UpdateTiming(DateTimeOffset startAt, DateTimeOffset endAt)
        {
            if (!IsEditable)
            {
                return WorkOrderErrors.TimingReadonly(Id.ToString(), State);
            }

            if (endAt <= startAt)
            {
                return WorkOrderErrors.InvalidTiming;
            }

            StartAtUtc = startAt;
            EndAtUtc = endAt;

            return Result.Updated;
        }
    }
}
