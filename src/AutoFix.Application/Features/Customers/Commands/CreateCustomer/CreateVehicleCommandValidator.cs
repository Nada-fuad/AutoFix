using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateVehicleCommandValidator:AbstractValidator<CreateVehicleCommand>

    {

        public CreateVehicleCommandValidator() {

            RuleFor(x => x.Make)
                 .NotEmpty().MaximumLength(50);

            RuleFor(x => x.Model)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.LicensePlate)
                .NotEmpty().MaximumLength(10);

          
        }
    }
}
