using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.UpdateRepairTask
{
    public sealed record UpdateRepairTaskPartCommand(Guid? PartId,
    string Name,
    decimal Cost,
    int Quantity
        );
  
}
