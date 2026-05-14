using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTasks
{
   public sealed record GetRepairTasksQuery : ICachedQuery<Result<List<RepairTaskDto>>>
    {
        public string CacheKey => "repair-tasks";

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);

        public string[] Tags => ["repair-tasks"];
    }
   
}
