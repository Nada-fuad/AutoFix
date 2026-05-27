using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders.Enums;
using AutoFix.Domain.WorkOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using AutoFix.Domain.WorkOrders.Events;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrder
{
    public class UpdateWorkOrderCommandHandler(IAppDbContext context,ILogger<UpdateWorkOrderCommandHandler> logger,HybridCache cache) : IRequestHandler<DeleteWorkOrderCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdateWorkOrderCommandHandler> _logger = logger;
        private readonly HybridCache _cache = cache;

        public async Task<Result<Deleted>> Handle(DeleteWorkOrderCommand command, CancellationToken ct)
        {

            var workOrder = await _context.WorkOrders.FirstOrDefaultAsync(x => x.Id == command.WorkOrderId,ct);

            if(workOrder is null)
            {
                _logger.LogError("WorkOrder with Id '{WorkOrderId}' does not exist.", command.WorkOrderId);
                return ApplicationErrors.WorkOrderNotFound;
            }

            if (workOrder.State is not WorkOrderState.Scheduled)
            {
                _logger.LogError(
                    "Deletion failed: only 'Scheduled' or 'Confirmed' WorkOrders can be deleted. Current status: {Status}",
                    workOrder.State);

                return WorkOrderErrors.Readonly;
            }

            _context.WorkOrders.Remove(workOrder);
           await  _context.SaveChangesAsync(ct);
            workOrder.AddDomainEvent(new WorkOrderCollectionModified());

            await _cache.RemoveByTagAsync("work-order", ct);

            return Result.Deleted;
        }
    }
}
