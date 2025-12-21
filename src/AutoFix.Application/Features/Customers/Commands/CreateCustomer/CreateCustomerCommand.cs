using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string PhoneNumber,
    string Email,
    List<CreateVehicleCommand> Vehicles

) : IRequest<Result<CustomerDto>>;