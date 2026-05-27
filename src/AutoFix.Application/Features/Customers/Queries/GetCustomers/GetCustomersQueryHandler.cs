using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Application.Features.Customers.Mappers;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomers
{
    public sealed class GetCustomersQueryHandler(IAppDbContext context) : IRequestHandler<GetCustomersQuery, Result<List<CustomerDto>>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<List<CustomerDto>>> Handle(GetCustomersQuery query, CancellationToken ct)
        {
            var customers = await _context.Customers.Include(c=>c.Vehicles).AsNoTracking().ToListAsync(ct);
            return customers.ToDtos();
        }

      
    }
}
