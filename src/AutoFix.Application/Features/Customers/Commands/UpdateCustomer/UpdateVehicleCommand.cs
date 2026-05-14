using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
   public sealed record UpdateVehicleCommand(Guid? VehicleId,
 string Make,
 string Model,
 int Year,
 string LicensePlate) : IRequest<Result<Updated>>;
}
