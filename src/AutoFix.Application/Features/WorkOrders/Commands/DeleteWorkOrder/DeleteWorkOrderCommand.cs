using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.DeleteWorkOrder;

public sealed record DeleteWorkOrderCommand(Guid WorkOrderId) : IRequest<Result<Deleted>>;