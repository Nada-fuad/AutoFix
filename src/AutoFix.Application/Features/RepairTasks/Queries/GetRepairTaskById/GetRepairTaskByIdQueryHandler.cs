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

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTaskById
{
    public class GetRepairTaskByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetRepairTaskByIdQuery, RepairTaskDto>
    {
        private readonly IAppDbContext _context = context;

        public async Task<RepairTaskDto> Handle(GetRepairTaskByIdQuery request, CancellationToken ct)
        {

            var repairTask = await _context.RepairTasks.Include(x => x.Parts).FirstOrDefaultAsync(x=>x.Id==request.repairTaskId,ct);

            if (repairTask is null)
            {
                return null;
            }



            return new RepairTaskDto
            {
                RepairTaskId = request.repairTaskId,
                Name = repairTask.Name,
                LaborCost = repairTask.LaborCost,
                EstimatedDurationInMins = repairTask.EstimatedDurationInMins,

                Parts = repairTask.Parts.Select(p => new PartDto
                {
                    Name = p.Name,
                    Cost = p.Cost,
                    Quantity = p.Quantity,
                }).ToList()

            };
        }
    }
}
