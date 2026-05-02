using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler (IAppDbContext context): IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId,cancellationToken);

            if (customer == null)
            {
               
            }
            return new CustomerDto
            {

                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
