using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask
{
   public sealed record CreateRepairTaskPartCommand(string Name,
    decimal Cost,
    int Quantity) :IRequest<Result<Success>>;
   
}
