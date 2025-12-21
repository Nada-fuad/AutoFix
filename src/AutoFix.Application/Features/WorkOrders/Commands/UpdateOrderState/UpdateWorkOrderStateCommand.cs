using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Workorders.Enums;

using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateOrderState;

public sealed record UpdateWorkOrderStateCommand(
    Guid WorkOrderId,
    WorkOrderState State) : IRequest<Result<Updated>>;