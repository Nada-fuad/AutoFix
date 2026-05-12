using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Application.Features.WorkOrders.Mappers;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrderById
{
    public class GetWorkOrderByIdQueryHandler (IAppDbContext context): IRequestHandler<GetWorkOrderByIdQuery, Result<WorkOrderDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<WorkOrderDto>> Handle(GetWorkOrderByIdQuery request, CancellationToken ct)
        {
            var workorder = await _context.WorkOrders.AsNoTracking()
                .Include(x => x.RepairTasks)
                .ThenInclude(p => p.Parts)
                .Include(x => x.Vehicle)
                .ThenInclude(c => c.Customer).FirstOrDefaultAsync(x=>x.Id==request.WorkOrderId);


            if(workorder is null)
            {
                return ApplicationErrors.WorkOrderNotFound;
            }
            return workorder.ToDto();
        }
    }
}
