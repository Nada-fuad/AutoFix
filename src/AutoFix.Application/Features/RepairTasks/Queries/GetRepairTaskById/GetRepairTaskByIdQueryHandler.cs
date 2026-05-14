using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Mappers;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTaskById
{
    public class GetRepairTaskByIdQueryHandler(IAppDbContext context,ILogger<GetRepairTaskByIdQueryHandler> logger) : IRequestHandler<GetRepairTaskByIdQuery, Result<RepairTaskDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetRepairTaskByIdQueryHandler> _logger = logger;

        public async Task<Result<RepairTaskDto>> Handle(GetRepairTaskByIdQuery query, CancellationToken ct)
        {

            var repairTask = await _context.RepairTasks.Include(x => x.Parts).FirstOrDefaultAsync(x=>x.Id== query.RepairTaskId, ct);

            if (repairTask is null)
            {
                _logger.LogWarning("Repair task with id {RepairTaskId} was not found", query.RepairTaskId);

                return ApplicationErrors.RepairTaskNotFound;
            }



            return repairTask.ToDto();

            
        }
    }
}
