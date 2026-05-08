using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.RemoveRepairTask
{
    public class RemoveRepairTaskCommandHandler(IAppDbContext context) : IRequestHandler<RemoveRepairTaskCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<Deleted>> Handle(RemoveRepairTaskCommand command, CancellationToken ct)
        {
            var taskRepairResult = await _context.RepairTasks.FindAsync(command.RepairTaskId, ct);
            if (taskRepairResult is null) { }

            _context.RepairTasks.Remove(taskRepairResult);

            await _context.SaveChangesAsync(ct);

            return Result.Deleted;
        }
    }
}
