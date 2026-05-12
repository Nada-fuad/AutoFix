using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Labors.Dtos;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Application.Features.Labors.Qieries.GetLabors
{
    public sealed record GetLaborsQuery : ICachedQuery<Result<List<LaborDto>>>
    {
        public string CacheKey => $"labors";

        public string[] Tags => ["labors"];

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);
    }
}
