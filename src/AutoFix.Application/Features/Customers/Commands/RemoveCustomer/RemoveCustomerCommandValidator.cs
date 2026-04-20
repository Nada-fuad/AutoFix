using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Commands.DeleteCustomer;
using FluentValidation;

namespace AutoFix.Application.Features.Customers.Commands.RemoveCustomer
{
   public class RemoveCustomerCommandValidator:AbstractValidator<RemoveCustomerCommand>
    {

        public RemoveCustomerCommandValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer Id is required");
        }
    }
}
