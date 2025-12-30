using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerValidator:AbstractValidator<UpdateCustomerCommand>
    {

        public UpdateCustomerValidator() {

            RuleFor(x => x.CustomerId)
            .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email")
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("Phone number is required.")
              .Matches(@"^\+?\d{7,15}$").WithMessage("Phone number must be 7–15 digits and may start with '+'.");


        }
    }
}
