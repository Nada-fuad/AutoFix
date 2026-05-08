using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask
{
    public class UpdateRepairTaskCommandHandler(IAppDbContext context) : IRequestHandler<UpdateRepairTaskCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<Updated>> Handle(UpdateRepairTaskCommand command, CancellationToken ct)
        {

            var repairTask = await _context.RepairTasks.Include(x => x.Parts).FirstOrDefaultAsync(x => x.Id == command.RepairTaskId, ct);
            if (repairTask is null)
            {

            }
            repairTask.Update(command.Name, command.LaborCost, command.EstimatedDurationInMins);

            foreach (var partCommand in command.Parts)
            {
                var part = repairTask.Parts.FirstOrDefault(p => p.Id == partCommand.PartId);
                

                var partResult = part.Update(part.Name, part.Cost, part.Quantity);
                await _context.SaveChangesAsync(ct);



            }
            return Result.Updated;
        }
        }
}
