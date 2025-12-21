using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer;

public sealed record CreateVehicleCommand(
 string Make,
 string Model,
 int Year,
 string LicensePlate) : IRequest<Result<VehicleDto>>;
