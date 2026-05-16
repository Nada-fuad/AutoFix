using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders.Enums;
using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.RecolateWorkOrder
{
    public sealed record RelocateWorkOrderCommand(Guid WorkOrderId, DateTimeOffset NewStartAt, Spot NewSpot):IRequest<Result<Updated>>;
    
}
