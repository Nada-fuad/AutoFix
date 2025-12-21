using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Enums;

using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask;

public sealed record CreateRepairTaskCommand(
    string? Name,
    decimal LaborCost,
    RepairDurationInMinutes? EstimatedDurationInMins,
    List<CreateRepairTaskPartCommand> Parts
) : IRequest<Result<RepairTaskDto>>;