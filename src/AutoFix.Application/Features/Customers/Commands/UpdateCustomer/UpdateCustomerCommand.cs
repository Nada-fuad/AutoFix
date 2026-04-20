using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(Guid CustomerId,
string Name,string Email,string PhoneNumber):IRequest<Result<Updated>>;
   
}
