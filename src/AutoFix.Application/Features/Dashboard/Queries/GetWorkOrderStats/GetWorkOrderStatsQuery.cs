using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Dashboard.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Dashboard.Queries.GetWorkOrderStats
{
    public sealed record GetWorkOrderStatsQuery(DateOnly Date) : IRequest<Result<TodayWorkOrderStatsDto>>;
}
