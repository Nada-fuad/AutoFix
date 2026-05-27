using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders.Enums;
using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.UpdateWorkOrderState
{
    public sealed record UpdateWorkOrderStateCommand(
      Guid WorkOrderId,
      WorkOrderState State) : IRequest<Result<Updated>>;
}
