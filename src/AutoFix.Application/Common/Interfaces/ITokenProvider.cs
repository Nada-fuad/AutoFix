using System.Security.Claims;

using AutoFix.Application.Features.Identity;
using AutoFix.Application.Features.Identity.Dtos;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Application.Common.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}