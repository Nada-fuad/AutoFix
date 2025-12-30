using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
    {

        public CreateCustomerCommandValidator() { 
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is invalid");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
        }
    }
}
