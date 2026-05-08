using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.RepairTasks.Dtos;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTaskById
{
    public sealed record GetRepairTaskByIdQuery(Guid repairTaskId):IRequest<RepairTaskDto>
    {
    }
}
