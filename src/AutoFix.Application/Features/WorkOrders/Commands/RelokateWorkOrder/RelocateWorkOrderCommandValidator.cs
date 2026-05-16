using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.WorkOrders.Commands.RecolateWorkOrder
{
    public  class RelocateWorkOrderCommandValidator:AbstractValidator<RelocateWorkOrderCommand> 
    {

        public RelocateWorkOrderCommandValidator()
        {
            RuleFor(x => x.WorkOrderId)
           .NotEmpty();

            RuleFor(x => x.NewStartAt)
                .GreaterThan(DateTimeOffset.UtcNow)
                .WithMessage("New start time must be in the future.");

            RuleFor(x => x.NewSpot)
                .IsInEnum();
        }
    }
}
