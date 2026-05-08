using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTasks
{
    public class GetRepairTasksQueryHandler(IAppDbContext context) : IRequestHandler<GetRepairTasksQuery, Result<List<RepairTaskDto>>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<List<RepairTaskDto>>> Handle(GetRepairTasksQuery request, CancellationToken ct)
        {
            var repairTasks = await _context.RepairTasks.Include(x => x.Parts).ToListAsync(ct);

            return repairTasks.Select(x => new RepairTaskDto
            {
                RepairTaskId = x.Id,
                Name = x.Name,
                LaborCost = x.LaborCost,
                EstimatedDurationInMins = x.EstimatedDurationInMins,

                Parts = x.Parts.Select(p => new PartDto { Name = p.Name, Cost = p.Cost, Quantity = p.Quantity }).ToList()
            }).ToList();

                
        }
    }
}
