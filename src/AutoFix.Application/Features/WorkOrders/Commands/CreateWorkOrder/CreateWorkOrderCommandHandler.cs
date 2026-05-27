using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Application.Features.WorkOrders.Mappers;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder
{
    public class CreateWorkOrderCommandHandler(ILogger<CreateWorkOrderCommandHandler> logger,IAppDbContext context, HybridCache cache, IWorkOrderPolicy workOrderValidator) : IRequestHandler<CreateWorkOrderCommand, Result<WorkOrderDto>>
    {
        private readonly ILogger<CreateWorkOrderCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;
        private readonly HybridCache _cache = cache;
        private readonly IWorkOrderPolicy _workOrderValidator = workOrderValidator;

        public async Task<Result<WorkOrderDto>> Handle(CreateWorkOrderCommand command, CancellationToken ct)
        {

            var repairTasks= await _context.RepairTasks.Where(x=>command.RepairTaskIds.Contains(x.Id)).ToListAsync(ct);

            if(repairTasks.Count != command.RepairTaskIds.Count) {

                var missingIds = command.RepairTaskIds.Except(repairTasks.Select(t => t.Id)).ToArray();

                _logger.LogError("Some RepairTaskIds not found: {MissingIds}", string.Join(", ", missingIds));

                return WorkOrderErrors.RepairTasksRequired;
            }
            var totalEstimatedDuration = TimeSpan.FromMinutes(repairTasks.Sum(r => (int)r.EstimatedDurationInMins));

            var endAt = command.StartAt.Add(totalEstimatedDuration);


            if (_workOrderValidator.IsOutsideOperatingHours(command.StartAt, totalEstimatedDuration))
            {
                _logger.LogError("The WorkOrder time ({StartAt} ? {EndAt}) is outside of store operating hours.", command.StartAt, endAt);

                return ApplicationErrors.WorkOrderOutsideOperatingHour(command.StartAt, endAt);
            }

            var checkMinRequirementResult = _workOrderValidator.ValidateMinimumRequirement(command.StartAt, endAt);

            if (checkMinRequirementResult.IsError)
            {
                _logger.LogError("WorkOrder duration is shorter than the configured minimum.");

                return checkMinRequirementResult.Errors;
            }

            var checkSpotAvailabilityResult = await _workOrderValidator.CheckSpotAvailabilityAsync(
           command.Spot,
           command.StartAt,
           endAt,
           excludeWorkOrderId: null,
           ct);


            if (checkSpotAvailabilityResult.IsError)
            {
                _logger.LogError("Spot: {Spot} is not available.", command.Spot.ToString());
                return checkSpotAvailabilityResult.Errors;
            }

            var vehicle = await _context.Vehicles.Include(v => v.Customer).FirstOrDefaultAsync(v => v.Id == command.VehicleId, cancellationToken: ct);

            if (vehicle is null)
            {
                _logger.LogError("Vehicle with Id '{VehicleId}' does not exist.", command.VehicleId);

                return ApplicationErrors.VehicleNotFound;
            }

            var labor = await _context.Employees.FindAsync([command.LaborId], ct);

            if (labor is null)
            {
                _logger.LogError("Invalid LaborId: {LaborId}", command.LaborId.ToString());
                return ApplicationErrors.LaborNotFound;
            }


            var hasVehicleConflict = await _context.WorkOrders
           .AnyAsync(
               a =>
               a.VehicleId == command.VehicleId &&
               a.StartAtUtc.Date == command.StartAt.Date &&
               a.StartAtUtc < endAt &&
               a.EndAtUtc > command.StartAt,
               ct);

            if (hasVehicleConflict)
            {
                _logger.LogError("Vehicle with Id '{VehicleId}' already has an overlapping WorkOrder.", command.VehicleId);
                return Error.Conflict(
                    code: "Vehicle_Overlapping_WorkOrders",
                    message: "The vehicle already has an overlapping WorkOrder.");
            }

            var isLaborOccupied = await _context.WorkOrders
                .AnyAsync(
                    a =>
                    a.LaborId == command.LaborId &&
                    a.StartAtUtc < endAt &&
                    a.EndAtUtc > command.StartAt,
                    ct);

            if (isLaborOccupied)
            {
                _logger.LogError("Labor with Id '{LaborId}' is already occupied during the requested time.", command.LaborId);
                return Error.Conflict(
                    code: "Labor_Occupied",
                    message: "Labor is already occupied during the requested time.");
            }

            var createWorkOrderResult = WorkOrder.Create(
          Guid.NewGuid(),
          command.VehicleId,
          command.StartAt,
          endAt,
          command.LaborId!,
          command.Spot,
          repairTasks);

            if (createWorkOrderResult.IsError)
            {
                _logger.LogError("Failed to create WorkOrder: {Error}", createWorkOrderResult.TopError.Message);

                return createWorkOrderResult.Errors;
            }

            var workOrder = createWorkOrderResult.Value;

            _context.WorkOrders.Add(workOrder);

            workOrder.AddDomainEvent(new WorkOrderCollectionModified());

            await _context.SaveChangesAsync(ct);

            workOrder.Vehicle = vehicle;
            workOrder.Labor = labor;

            _logger.LogInformation("WorkOrder with Id '{WorkOrderId}' created successfully.", workOrder.Id);

            await _cache.RemoveByTagAsync("work-order", ct);

            return workOrder.ToDto();

        }
    }
}
