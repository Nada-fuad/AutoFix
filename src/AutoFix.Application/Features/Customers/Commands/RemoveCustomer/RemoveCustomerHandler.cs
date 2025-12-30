using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer
{
    public class RemoveCustomerHandler(IAppDbContext context) : IRequestHandler<RemoveCustomerCommand, Result<Deleted>>
    {

        private readonly IAppDbContext _context=context;
        public async Task<Result<Deleted>> Handle(RemoveCustomerCommand request, CancellationToken ct)
        {
           var customer =await  _context.Customers.FindAsync(request.customerId, ct);

            if (customer is null)
            {
                return CustomerErrors.NotFound;
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync(ct);


            return Result.Deleted;

        }
    }
}
