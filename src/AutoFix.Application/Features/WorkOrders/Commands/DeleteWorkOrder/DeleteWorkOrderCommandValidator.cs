using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrder
{
    public class DeleteWorkOrderCommandValidator:AbstractValidator<DeleteWorkOrderCommand>
    {

       public DeleteWorkOrderCommandValidator() {


            RuleFor(x => x.WorkOrderId)
              .NotEmpty()
              .WithErrorCode("WorkOrderId_Required")
              .WithMessage("WorkOrderId is required.");
        } 
    }
}
