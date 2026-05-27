using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.RecolateWorkOrder
{
    public class RecolateWorkOrderCommandHandler(IAppDbContext context,HybridCache cache,ILogger<RecolateWorkOrderCommandHandler> logger,IWorkOrderPolicy workOrderValidator
        ) : IRequestHandler<RelocateWorkOrderCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly HybridCache _cache = cache;
        private readonly ILogger<RecolateWorkOrderCommandHandler> _logger = logger;
        private readonly IWorkOrderPolicy _appointmentValidator = workOrderValidator;

        public async Task<Result<Updated>> Handle(RelocateWorkOrderCommand command, CancellationToken ct)
        {
            

            var workOrder = await _context.WorkOrders.
                Include(a=>a.RepairTasks).
                Include(a=>a.Labor).
                Include(a=>a.Vehicle).
                FirstOrDefaultAsync(a => a.Id == command.WorkOrderId, ct);


            if (workOrder is null)
            {
                _logger.LogError("WorkOrder with Id '{WorkOrderId}' does not exist.", command.WorkOrderId);

                return ApplicationErrors.WorkOrderNotFound;
            }

            var duration = workOrder.EndAtUtc.Subtract(workOrder.StartAtUtc).Duration();

            var endAt = command.NewStartAt.Add(duration);

            var checkSpotAvailabilityResult = await _appointmentValidator.CheckSpotAvailabilityAsync(
           workOrder.Spot,
           command.NewStartAt,
           endAt,
           excludeWorkOrderId: workOrder.Id,
           ct);

            if (checkSpotAvailabilityResult.IsError)
            {
                _logger.LogError("Spot: {Spot} is not available.", workOrder.Spot.ToString());

                return checkSpotAvailabilityResult.Errors;
            }

            if (await _appointmentValidator.IsLaborOccupied(workOrder.LaborId!.Value, command.WorkOrderId, command.NewStartAt, endAt))
            {
                _logger.LogError("Labor with Id '{LaborId}' is already occupied during the requested time.", workOrder.LaborId);

                return ApplicationErrors.LaborOccupied;
            }

            if (await _appointmentValidator.IsVehicleAlreadyScheduled(workOrder.VehicleId, command.NewStartAt, endAt, command.WorkOrderId))
            {
                _logger.LogError("Vehicle with Id '{VehicleId}' already has an overlapping WorkOrder.", workOrder.VehicleId);

                return ApplicationErrors.VehicleSchedulingConflict;
            }

            var updateTimingResult = workOrder.UpdateTiming(command.NewStartAt, endAt);

            if (updateTimingResult.IsError)
            {
                _logger.LogError("Failed to update timing: {Error}", updateTimingResult.TopError.Message);

                return updateTimingResult.Errors;
            }


          

            var updateSpotResult = workOrder.UpdateSpot(command.NewSpot);

            if (updateTimingResult.IsError)
            {
                _logger.LogError("Failed to update Spot: {Error}", updateSpotResult.TopError.Message);

                return updateTimingResult.Errors;
            }

            workOrder.AddDomainEvent(new WorkOrderCollectionModified());

            await _context.SaveChangesAsync(ct);

            await _cache.RemoveByTagAsync("work-order", ct);


            return Result.Updated;
        }
    }
}
