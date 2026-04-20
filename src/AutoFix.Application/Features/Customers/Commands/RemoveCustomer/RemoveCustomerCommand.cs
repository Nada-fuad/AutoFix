using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.DeleteCustomer
{
    public sealed record RemoveCustomerCommand(Guid CustomerId) :IRequest<Result<Deleted>>;
    
}
