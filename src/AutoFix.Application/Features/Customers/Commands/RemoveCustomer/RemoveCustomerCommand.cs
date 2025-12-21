using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer;

public sealed record RemoveCustomerCommand(Guid CustomerId)
    : IRequest<Result<Deleted>>;