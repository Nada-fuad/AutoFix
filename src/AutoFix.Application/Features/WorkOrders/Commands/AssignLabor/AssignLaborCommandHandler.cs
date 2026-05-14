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
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Commands.AssignLabor
{
    public class AssignLaborCommandHandler(IAppDbContext context,ILogger<AssignLaborCommandHandler> logger) : IRequestHandler<AssignLaborCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;

        public ILogger<AssignLaborCommandHandler> _logger { get; } = logger;

        public async Task<Result<Updated>> Handle(AssignLaborCommand command, CancellationToken ct)
        {

            var workOrder= await _context.WorkOrders.FirstOrDefaultAsync(w=>w.Id==command.WorkOrderId,ct);
            if (workOrder is null)
            {
                _logger.LogError("WorkOrder with Id '{WorkOrderId}' does not exist.", command.WorkOrderId);
                return ApplicationErrors.WorkOrderNotFound;
            }

            var labor = await _context.Employees.FindAsync([command.LaborId],ct);
            if (labor is null)
            {
                _logger.LogError("Invalid LaborId: {LaborId}", command.LaborId);
                return ApplicationErrors.LaborNotFound;
            }



            var updateLaborResult = workOrder.UpdateLabor(command.LaborId);

            if (updateLaborResult.IsError)
            {
                foreach (var error in updateLaborResult.Errors)
                {
                    _logger.LogError("[LaborUpdate] {ErrorCode}: {ErrorDescription}", error.Code, error.Message);
                }

                return updateLaborResult.Errors;
            }
            await _context.SaveChangesAsync(ct);

            return Result.Updated;
        }
    }
}
