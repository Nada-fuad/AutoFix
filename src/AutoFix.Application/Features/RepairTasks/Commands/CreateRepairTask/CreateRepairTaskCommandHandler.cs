using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Mappers;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.RepairTasks.Parts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask
{
    public class CreateRepairTaskCommandHandler(IAppDbContext context,ILogger<CreateRepairTaskCommandHandler> logger,HybridCache cache) : IRequestHandler<CreateRepairTaskCommand, Result<RepairTaskDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateRepairTaskCommandHandler> _logger = logger;
        private readonly HybridCache _cache = cache;

        public async Task<Result<RepairTaskDto>> Handle(CreateRepairTaskCommand command, CancellationToken ct)
        {



            var nameExists = await _context.RepairTasks
           .AnyAsync(p => EF.Functions.Like(p.Name, command.Name), ct);

            if (nameExists)
            {
                _logger.LogWarning("Duplicate part name '{PartName}'.", command.Name);

                return RepairTaskErrors.DuplicateName;
            }

            List<Part> parts = [];

            foreach (var partCommand in command.Parts
                )
            {
                var partResult = Part.Create(Guid.NewGuid(),partCommand.Name,partCommand.Cost,partCommand.Quantity);

                if (partResult.IsError)
                {
                    return partResult.Errors;
                }

                parts.Add(partResult.Value);
            }
            var repairTasResult = RepairTask.Create(Guid.NewGuid(), command.Name, command.LaborCost, command.EstimatedDurationInMins,parts);
            
            if (repairTasResult.IsError)
            {
                return repairTasResult.Errors;
            }
            var repairTask = repairTasResult.Value;
            await _context.RepairTasks.AddAsync(repairTask, ct);
            await _context.SaveChangesAsync(ct);
            await _cache.RemoveByTagAsync("repair-task", ct);

            
            return repairTask.ToDto(); ;
            

        }
    }
}
