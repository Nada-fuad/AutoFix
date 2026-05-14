using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.WorkOrders.Commands.AssignLabor
{
    public class AssignLaborCommandValidator:AbstractValidator<AssignLaborCommand>
    {
        public AssignLaborCommandValidator() {


            RuleFor(x => x.WorkOrderId)
            .NotEmpty()
            .WithErrorCode("WorkOrderId_Required")
            .WithMessage("WorkOrderId is required.");

            RuleFor(x => x.LaborId)
               .NotEmpty()
               .WithErrorCode("LaborId_Required")
               .WithMessage("LaborId is required.");
        }
    }
}
