using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Enums;
using AutoFix.Domain.WorkOrders.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrderState
{
    public class UpdateWorkOrderStateCommandHandler(IAppDbContext context, ILogger<UpdateWorkOrderStateCommandHandler> logger,HybridCache cache,TimeProvider dateTime):IRequestHandler<UpdateWorkOrderStateCommand,Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdateWorkOrderStateCommandHandler> _logger = logger;
        private readonly HybridCache _cache = cache;
        private readonly TimeProvider _dateTime = dateTime;

        public async Task<Result<Updated>> Handle(UpdateWorkOrderStateCommand command, CancellationToken ct)
        {
            var workOrder = await _context.WorkOrders
            .FirstOrDefaultAsync(a => a.Id == command.WorkOrderId, ct);

        if (workOrder is null)
        {
            _logger.LogError("WorkOrder with Id '{WorkOrderId}' does not exist.", command.WorkOrderId);

            return ApplicationErrors.WorkOrderNotFound;
        }

        if (workOrder.StartAtUtc > _dateTime.GetUtcNow())
        {
            _logger.LogError("State transition for WorkOrder Id '{WorkOrderId}` is not allowed before the work order�s scheduled start time.", command.WorkOrderId);

            return WorkOrderErrors.StateTransitionNotAllowed(workOrder.StartAtUtc);
        }

        var updateStatusResult = workOrder.UpdateState(command.State);

        if (updateStatusResult.IsError)
        {
            _logger.LogError("Failed to update status: {Error}", updateStatusResult.TopError.Message);

            return updateStatusResult.Errors;
        }

        if (command.State == WorkOrderState.Completed)
        {
            workOrder.AddDomainEvent(new WorkOrderCompleted { WorkOrderId = command.WorkOrderId });
        }

        await _context.SaveChangesAsync(ct);

        workOrder.AddDomainEvent(new WorkOrderCollectionModified());

        await _cache.RemoveByTagAsync("work-order", ct);

        return Result.Updated;
        }
    }
}
