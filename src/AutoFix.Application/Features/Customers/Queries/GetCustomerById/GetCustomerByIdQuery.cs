using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomerById
{
   public sealed record GetCustomerByIdQuery(Guid CustomerId) : ICachedQuery<Result<CustomerDto>>
    {
        public string CacheKey => $"customer_{CustomerId}";

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);

        public string[] Tags => ["customer"];
    }
   
}
