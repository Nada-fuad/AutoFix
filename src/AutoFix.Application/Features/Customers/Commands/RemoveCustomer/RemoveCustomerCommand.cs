using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer
{
   public sealed record RemoveCustomerCommand(Guid customerId):IRequest<Result<Deleted>>

    {
    }
}
