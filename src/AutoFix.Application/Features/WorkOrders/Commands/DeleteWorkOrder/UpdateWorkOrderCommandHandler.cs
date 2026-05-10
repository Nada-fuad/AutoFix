using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrder
{
    public class UpdateWorkOrderCommandHandler(IAppDbContext context) : IRequestHandler<DeleteWorkOrderCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<Deleted>> Handle(DeleteWorkOrderCommand command, CancellationToken ct)
        {

            var workOrder = await _context.WorkOrders.FirstOrDefaultAsync(x => x.Id == command.WorkOrderId,ct);

            if(workOrder == null)
            {
                return null;
            }

            _context.WorkOrders.Remove(workOrder);
            _context.SaveChangesAsync(ct);

                return Result.Deleted;
        }
    }
}
