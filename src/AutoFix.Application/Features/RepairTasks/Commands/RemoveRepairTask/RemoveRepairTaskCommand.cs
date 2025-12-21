using FluentValidation;

using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.RemoveRepairTask;

public sealed record RemoveRepairTaskCommand(Guid RepairTaskId)
    : IRequest<Result<Deleted>>;

public class RemoveRepairTaskCommandValidator : AbstractValidator<RemoveRepairTaskCommand>
{
    public RemoveRepairTaskCommandValidator()
    {
        RuleFor(x => x.RepairTaskId)
            .NotEmpty().WithMessage("Repair task Id is required.");
    }
}