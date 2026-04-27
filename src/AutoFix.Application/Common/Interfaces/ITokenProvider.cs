using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Identity.Dtos;
using AutoFix.Application.Features.Identity;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Application.Common.Interfaces
{
   public interface ITokenProvider
    {

        Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);


    }
}
