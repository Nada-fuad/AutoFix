using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Workorders.Enums;

using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.RelocateWorkOrder;

public sealed record RelocateWorkOrderCommand(
    Guid WorkOrderId,
    DateTimeOffset NewStartAt,
    Spot NewSpot) : IRequest<Result<Updated>>;