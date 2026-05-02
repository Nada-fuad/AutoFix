using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using MediatR;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery:IRequest<List<CustomerDto>>;
   
}
