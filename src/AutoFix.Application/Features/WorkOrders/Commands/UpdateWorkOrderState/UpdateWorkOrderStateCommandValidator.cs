using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrderState
{
    public sealed class UpdateWorkOrderStateCommandValidator : AbstractValidator<UpdateWorkOrderStateCommand>
    {
        public UpdateWorkOrderStateCommandValidator()
        {
            RuleFor(x => x.State)
               .IsInEnum()
               .WithErrorCode("WorkOrderStatus_Invalid")
               .WithMessage("Status must be a valid WorkOrderStatus value.");
        }
    }
}
