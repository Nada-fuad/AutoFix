using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.WorkOrders.Queries.GetWorkOrderById
{
    public sealed record GetWorkOrderByIdQuery(Guid WorkOrderId) : ICachedQuery<Result<WorkOrderDto>>
    {
        public string CacheKey => $"work-order:{WorkOrderId}";

        public string[] Tags => ["work-order"];

        public TimeSpan Expiration =>TimeSpan.FromMinutes(10);
    }
}
