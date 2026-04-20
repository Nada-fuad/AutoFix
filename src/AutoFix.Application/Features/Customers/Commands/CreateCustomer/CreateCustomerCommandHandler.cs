using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Application.Features.Customers.Mappers;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler(IAppDbContext context,ILogger<CreateCustomerCommandHandler> logger) : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateCustomerCommandHandler> _logger = logger;

        public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand command, CancellationToken ct)
        {

            var email = command.email.Trim().ToLower();

            var exists= await _context.Customers.AnyAsync(x => x.Email!.ToLower() == email,ct);

            if (exists)
            {
                _logger.LogWarning("Customer creation aborted. Email already exists");

                return CustomerErrors.CustomerExists;
            }
            var createCustomerResult = Customer.Create(Guid.NewGuid(),   command.name.Trim(), command.email.Trim(), command.phoneNumber.Trim());

            if (createCustomerResult.IsError)
            {
                return createCustomerResult.Errors;
            }

            _context.Customers.Add(createCustomerResult.Value);

            await _context.SaveChangesAsync(ct);

            var customer = createCustomerResult.Value;
            _logger.LogInformation("Customer created successfully. Id: {CustomerId}", createCustomerResult.Value.Id);

            var dto = CustomerMapper.ToDto(customer);
            return dto;


          
                
        }
    }
}
