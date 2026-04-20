using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger,IAppDbContext context) : IRequestHandler<UpdateCustomerCommand, Result<Updated>>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<Updated>> Handle(UpdateCustomerCommand command, CancellationToken ct)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == command.CustomerId,ct);

            if (customer is null)
            {
                _logger.LogWarning("Customer {CustomerId} not found for update.", command.CustomerId);

                return CustomerErrors.NameRequired;
            }

            var updateCustomerResult = customer.Update(command.Name, command.Email, command.PhoneNumber);
            if (updateCustomerResult.IsError)
            {
                return updateCustomerResult.Errors;
            }
            await _context.SaveChangesAsync(ct);
            return Result.Updated;
        }
    }
}
