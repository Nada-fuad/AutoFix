using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Customers.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdValiditor: AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdValiditor() {

            RuleFor(x => x.CustomerId).NotEmpty();
        
        }
    }
}
