using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.RepairTasks.Commands.RemoveRepairTask
{
   public class RemoveRepairTaskCommandValidator:AbstractValidator<RemoveRepairTaskCommand>

    {

        public RemoveRepairTaskCommandValidator() {

            RuleFor(x => x.RepairTaskId)
               .NotEmpty().WithMessage("Repair task Id is required.");
        }

    }
}
