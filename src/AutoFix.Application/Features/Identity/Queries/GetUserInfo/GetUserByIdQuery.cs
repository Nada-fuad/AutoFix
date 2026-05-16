using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Identity.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Identity.Queries.GetUserInfo
{
   public sealed record  GetUserByIdQuery(string? UserId):IRequest<Result<AppUserDto>>
    {
    }
}
