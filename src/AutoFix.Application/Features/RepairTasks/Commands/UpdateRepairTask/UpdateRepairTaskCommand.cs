using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Enums;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask
{
    public sealed record UpdateRepairTaskCommand(Guid RepairTaskId, string Name, decimal LaborCost,
    RepairDurationInMinutes EstimatedDurationInMins, List<UpdateRepairTaskPartCommand> Parts ) :IRequest<Result<Updated>>
    {
    }
}
