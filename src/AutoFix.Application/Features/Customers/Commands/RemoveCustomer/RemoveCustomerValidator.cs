using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer
{
    public class RemoveCustomerValidator:AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerValidator() {
        
        RuleFor(x=>x.customerId).NotEmpty().WithMessage("Customer Id is required.");

        }
    }
}
