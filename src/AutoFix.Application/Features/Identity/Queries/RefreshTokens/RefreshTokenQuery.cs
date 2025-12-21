using AutoFix.Domain.Common.Results;

using MediatR;

namespace AutoFix.Application.Features.Identity.Queries.RefreshTokens;

public record RefreshTokenQuery(string RefreshToken, string ExpiredAccessToken) : IRequest<Result<TokenResponse>>;