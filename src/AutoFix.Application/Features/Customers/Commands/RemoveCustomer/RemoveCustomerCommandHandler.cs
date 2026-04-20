using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer
{
    public class RemoveCustomerCommandHandler(ILogger<RemoveCustomerCommandHandler> logger, IAppDbContext context) : IRequestHandler<RemoveCustomerCommand, Result<Deleted>>
    {
        private readonly ILogger<RemoveCustomerCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<Deleted>> Handle(RemoveCustomerCommand command, CancellationToken ct)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id==command.CustomerId);

            if (customer is null)
            {
                return CustomerErrors.NameRequired;
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(ct);
            return Result.Deleted;
        }
    }
}
