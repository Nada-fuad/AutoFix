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

namespace AutoFix.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler (IAppDbContext context,ILogger<GetCustomerByIdHandler> logger): IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetCustomerByIdHandler> _logger = logger;

        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery query, CancellationToken ct)
        {

            var customer = await _context.Customers.AsNoTracking().Include(v=>v.Vehicles).FirstOrDefaultAsync(x => x.Id == query.CustomerId,ct);

            if (customer == null)
            {
                _logger.LogWarning("Customer with id {CustomerId} was not found", query.CustomerId);
                return Error.NotFound(
                code: "Customer_NotFound",
                message: $"Customer with id '{query.CustomerId}' was not found");
            }
            return customer.ToDto();
        }
    }
}
