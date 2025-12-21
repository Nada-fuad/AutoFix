using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Mappers;
using AutoFix.Domain.Common.Results;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.RepairTasks.Queries.GetRepairTasks;

public class GetRepairTasksQueryHandler(IAppDbContext context)
    : IRequestHandler<GetRepairTasksQuery, Result<List<RepairTaskDto>>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<List<RepairTaskDto>>> Handle(GetRepairTasksQuery query, CancellationToken ct)
    {
        var repairTasks = await _context.RepairTasks.Include(rt => rt.Parts).AsNoTracking().ToListAsync(ct);

        return repairTasks.ToDtos();
    }
}