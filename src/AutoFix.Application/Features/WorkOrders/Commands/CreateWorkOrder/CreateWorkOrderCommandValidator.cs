using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder
{
    public class CreateWorkOrderCommandValidator:AbstractValidator<CreateWorkOrderCommand>
    {

        public CreateWorkOrderCommandValidator() { 
        
        
        RuleFor(x=>x.VehicleId
        ).NotEmpty().WithMessage("VehicleId is required.");


            RuleFor(request => request.StartAt)
            .GreaterThan(DateTimeOffset.UtcNow)
            .WithMessage("StartAt must be in the future.");

            RuleFor(request => request.RepairTaskIds)
                .NotEmpty()
                .WithMessage("At least one repair task must be selected");


            RuleFor(request => request.LaborId)
                .Must(laborId => laborId is null || laborId != Guid.Empty)
                .WithMessage("If provided, LaborId must not be empty.");



        }
    }
}
