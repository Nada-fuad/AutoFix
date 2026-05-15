using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed record  CreateCustomerCommand(string Name,string Email,string PhoneNumber,List<CreateVehicleCommand>  Vehicles) :IRequest<Result<CustomerDto>>;
    
}
