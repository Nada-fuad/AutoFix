using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrderRepairTasks
{
    public class UpdateWorkOrderRepairTasksCommandHandler(IAppDbContext context ,ILogger<UpdateWorkOrderRepairTasksCommandHandler> logger,IWorkOrderPolicy workOrderPolicy) : IRequestHandler<UpdateWorkOrderRepairTasksCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdateWorkOrderRepairTasksCommandHandler> _logger = logger;
        private readonly IWorkOrderPolicy _workOrderValidator = workOrderPolicy;

        public async Task<Result<Updated>> Handle(UpdateWorkOrderRepairTasksCommand command, CancellationToken ct)
        {

            var workOrder = await _context.WorkOrders.Include(w=>w.RepairTasks).FirstOrDefaultAsync(w => w.Id == command.WorkOrderId,ct);

            if (workOrder is null)
            {
                logger.LogError("WorkOrder with Id '{WorkOrderId}' does not exist.", command.WorkOrderId);

                return ApplicationErrors.WorkOrderNotFound;
            }


            if (command.RepairTaskIds.Length == 0)
            {
                logger.LogError("Empty RepairTaskIds list submitted.");

                return ApplicationErrors.AtLeastOneRepairTaskIsRequired;
            }

            var requestedTasks = await _context.RepairTasks.Where(w=>command.RepairTaskIds.Contains(w.Id)).ToListAsync(ct);

            if (requestedTasks.Count != command.RepairTaskIds.Length)
            {
                var missingIds = command.RepairTaskIds.Except(requestedTasks.Select(t => t.Id)).ToArray();

                logger.LogError("One or more RepairTasks not found. {ids}", string.Join(", ", missingIds));

                return ApplicationErrors.RepairTaskNotFound;
            }

            var clearExistingResult = workOrder.ClearRepairTasks();

            if (clearExistingResult.IsError)
            {
                return clearExistingResult;
            }

            foreach (var task in requestedTasks)
            {
                var addRepairTaskResult = workOrder.AddRepairTask(task);

                if (addRepairTaskResult.IsError)
                {
                    return addRepairTaskResult;
                }
            }


            var totalDuration = TimeSpan.FromMinutes(requestedTasks.Sum(x => (int)x.EstimatedDurationInMins));

            var newEndAt = workOrder.StartAtUtc + totalDuration;

            // Business validations
            if (_workOrderValidator.IsOutsideOperatingHours(workOrder.StartAtUtc, totalDuration))
            {
                return Error.Conflict("WorkOrder_Outside_OperatingHours", "WorkOrder timing exceeds business hours.");
            }

            var spotCheckResult = await _workOrderValidator.CheckSpotAvailabilityAsync(
                workOrder.Spot,
                workOrder.StartAtUtc,
                newEndAt,
                excludeWorkOrderId: workOrder.Id,
                ct: ct);

            if (spotCheckResult.IsError)
            {
                return spotCheckResult.Errors;
            }

            if (await _workOrderValidator.IsLaborOccupied(workOrder.LaborId!.Value, workOrder.Id, workOrder.StartAtUtc, newEndAt))
            {
                return ApplicationErrors.LaborOccupied;
            }

            workOrder.UpdateTiming(workOrder.StartAtUtc, newEndAt);

            workOrder.AddDomainEvent(new WorkOrderCollectionModified());

            await context.SaveChangesAsync(ct);

            workOrder.AddDomainEvent(new WorkOrderCollectionModified());

            return Result.Updated;
        }
    }
}
