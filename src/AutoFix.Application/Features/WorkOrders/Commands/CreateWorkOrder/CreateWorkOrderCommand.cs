using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks;
using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder
{
    public sealed record CreateWorkOrderCommand(Guid VehicleId, DateTimeOffset StartAt, List<Guid> RepairTaskIds, Guid? LaborId) :IRequest<Result<WorkOrderDto>>;
    
}
