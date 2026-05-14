using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Customers.Dtos;
using MediatR;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomers
{
    public sealed record GetCustomersQuery : ICachedQuery<List<CustomerDto>>
    {
        public string CacheKey => "customers";
    public string[] Tags => ["customer"];

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);
    }
   
}
