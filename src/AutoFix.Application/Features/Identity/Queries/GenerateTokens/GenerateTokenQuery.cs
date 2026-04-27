using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Identity.Queries.GenerateTokens
{
    public record GenerateTokenQuery(
     string Email,
     string Password) : IRequest<Result<TokenResponse>>;
}
