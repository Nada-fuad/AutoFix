using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Labors.Dtos;
using AutoFix.Application.Features.Labors.Mappers;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Labors.Qieries.GetLabors
{
    public class GetLaborsQueryHandler(IAppDbContext contect) : IRequestHandler<GetLaborsQuery, Result<List<LaborDto>>>
    {
        private readonly IAppDbContext _contect = contect;

        public async Task<Result<List<LaborDto>>> Handle(GetLaborsQuery request, CancellationToken ct)
        {
           var labors= await _contect.Employees.AsNoTracking().Where(e => e.Role == Role.Labor).ToListAsync(ct);

            return labors.ToDtos();
        }
    }
}
