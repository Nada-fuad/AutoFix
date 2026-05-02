using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomers
{
    public sealed class GetCustomersQueryHandler(IAppDbContext context) : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<List<CustomerDto>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ToListAsync(cancellationToken);
            return customers.Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }
    }
}
