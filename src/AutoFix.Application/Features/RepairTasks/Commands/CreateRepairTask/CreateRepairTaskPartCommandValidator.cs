using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask
{
    public class CreateRepairTaskPartCommandValidator:AbstractValidator<CreateRepairTaskPartCommand>
    {

        public CreateRepairTaskPartCommandValidator() {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Part name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Cost)
                .GreaterThan(0).WithMessage("Part cost must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");


        }
    }
}
