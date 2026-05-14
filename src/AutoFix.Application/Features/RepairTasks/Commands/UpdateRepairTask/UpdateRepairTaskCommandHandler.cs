using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Parts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask
{
    public class UpdateRepairTaskCommandHandler(IAppDbContext context,ILogger<UpdateRepairTaskCommandHandler> logger,HybridCache cache) : IRequestHandler<UpdateRepairTaskCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdateRepairTaskCommandHandler> _logger = logger;
        private readonly HybridCache _cache = cache;

        public async Task<Result<Updated>> Handle(UpdateRepairTaskCommand command, CancellationToken ct)
        {

            var repairTask = await _context.RepairTasks.Include(x => x.Parts).FirstOrDefaultAsync(x => x.Id == command.RepairTaskId, ct);
            if (repairTask is null)
            {
                _logger.LogWarning("RepairTask {RepairTaskId} not found for update.", command.RepairTaskId);

                return ApplicationErrors.RepairTaskNotFound;
            }
            var validatedParts = new List<Part>();

            foreach (var p in command.Parts)
            {
                var partId = p.PartId ?? Guid.NewGuid();

                var partResult = Part.Create(partId, p.Name, p.Cost, p.Quantity);

                if (partResult.IsError)
                {
                    return partResult.Errors;
                }

                validatedParts.Add(partResult.Value);
            }

            var updateRepairTaskResult = repairTask.Update(command.Name, command.LaborCost, command.EstimatedDurationInMins);

            if (updateRepairTaskResult.IsError)
            {
                return updateRepairTaskResult.Errors;
            }

            var upsertPartsResult = repairTask.UpserParts(validatedParts);

            if (upsertPartsResult.IsError)
            {
                return upsertPartsResult.Errors;
            }

            await _context.SaveChangesAsync(ct);
            await _cache.RemoveByTagAsync("repair-task", ct);

            return Result.Updated;
        }
        }
}
