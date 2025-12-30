using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IAppDbContext context) : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var createCustomerResult = Customer.Create(
                Guid.NewGuid(),
                request.Name.Trim(),
                request.PhoneNumber.Trim(),
                request.Email.Trim());

            if (createCustomerResult.IsError)
            {
                return createCustomerResult.Errors;
            }

            var customer = createCustomerResult.Value;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = new CustomerDto
            {
                CustomerId = customer.Id,
                Name = customer.Name!,
                PhoneNumber = customer.PhoneNumber!,
                Email = customer.Email!
            };

            return dto;
        }
    }
}
