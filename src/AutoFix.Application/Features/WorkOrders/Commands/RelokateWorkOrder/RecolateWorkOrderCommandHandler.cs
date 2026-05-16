using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.RecolateWorkOrder
{
    public class RecolateWorkOrderCommandHandler(IAppDbContext context,HybridCache cache,ILogger<RecolateWorkOrderCommandHandler> logger
        ) : IRequestHandler<RelocateWorkOrderCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly HybridCache _cache = cache;
        private readonly ILogger<RecolateWorkOrderCommandHandler> _logger = logger;

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
            await _context.SaveChangesAsync(ct);

            await _cache.RemoveByTagAsync("work-order", ct);


            return Result.Updated;
        }
    }
}
