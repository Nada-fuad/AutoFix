using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed record class CreateCustomerCommand(string Name, string Email, string PhoneNumber):IRequest<Result<CustomerDto>>;

}
