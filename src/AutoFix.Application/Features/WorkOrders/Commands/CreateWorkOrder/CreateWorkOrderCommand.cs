using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Workorders.Enums;

using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder;

public sealed record CreateWorkOrderCommand(
    Spot Spot,
    Guid VehicleId,
    DateTimeOffset StartAt,
    List<Guid> RepairTaskIds,
    Guid? LaborId)
: IRequest<Result<WorkOrderDto>>;