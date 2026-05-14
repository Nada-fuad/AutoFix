using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.AssignLabor
{
    public sealed record AssignLaborCommand(Guid WorkOrderId,Guid LaborId):IRequest<Result<Updated>>;
    
}
