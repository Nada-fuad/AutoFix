using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Enums;
using AutoFix.Domain.RepairTasks.Parts;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask
{
    public sealed record CreateRepairTaskCommand(string Name, decimal LaborCost, RepairDurationInMinutes EstimatedDurationInMins, List<CreateRepairTaskPartCommand> Parts) : IRequest<Result<RepairTaskDto>>;

}