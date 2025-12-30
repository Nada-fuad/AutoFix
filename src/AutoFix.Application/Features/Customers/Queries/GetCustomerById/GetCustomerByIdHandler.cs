using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomerById
{
    internal class GetCustomerByIdHandler(IAppDbContext context) : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
    {

        private readonly IAppDbContext _context=context;
        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken ct)
        {
            var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.CustomerId, ct);
            if (customer == null)
            {
                return CustomerErrors.NotFound;

            }
            var dto= new CustomerDto { 
            
            CustomerId =customer.Id,
            Name=customer.Name,
            PhoneNumber=customer.PhoneNumber,
            Email=customer.Email,
            
            };

            return dto;
        }
    }
}
