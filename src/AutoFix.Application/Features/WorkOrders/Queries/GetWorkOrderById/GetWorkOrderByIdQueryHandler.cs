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
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrderById
{
    public class GetWorkOrderByIdQueryHandler (IAppDbContext context,ILogger<GetWorkOrderByIdQueryHandler> logger): IRequestHandler<GetWorkOrderByIdQuery, Result<WorkOrderDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetWorkOrderByIdQueryHandler> _logger = logger;

        public async Task<Result<WorkOrderDto>> Handle(GetWorkOrderByIdQuery query, CancellationToken ct)
        {
            var workorder = await _context.WorkOrders.AsNoTracking()
                .Include(x => x.RepairTasks)
                .ThenInclude(p => p.Parts)
                .Include(a => a.Labor)

                .Include(x => x.Vehicle)
                .ThenInclude(c => c.Customer)
                .Include(a => a.Invoice).



                FirstOrDefaultAsync(x=>x.Id==query.WorkOrderId,ct);


            if(workorder is null)
            {
                _logger.LogWarning("WorkOrder with id {WorkOrderId} was not found", query.WorkOrderId);

                return ApplicationErrors.WorkOrderNotFound;
            }
            return workorder.ToDto();
        }
    }
}
