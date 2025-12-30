using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerHandler(IAppDbContext context) : IRequestHandler<UpdateCustomerCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context=context;
        public async Task<Result<Updated>> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {

            var customer = await _context.Customers.FindAsync(request.CustomerId,ct);

            if (customer is null)
                return CustomerErrors.NotFound;

            customer.Update(request.Name, request.PhoneNumber, request.Email);
            await _context.SaveChangesAsync(ct);

            return Result.Updated;
        }
    }
}
