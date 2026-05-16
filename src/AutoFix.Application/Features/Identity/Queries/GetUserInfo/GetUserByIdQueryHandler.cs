using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Identity.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Identity.Queries.GetUserInfo
{
    public  class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger,IIdentityService identityService) : IRequestHandler<GetUserByIdQuery, Result<AppUserDto>>
    {
        private readonly ILogger<GetUserByIdQueryHandler> _logger = logger;
        private readonly IIdentityService _identityService = identityService;

        public async Task<Result<AppUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var getUserByIdResult = await _identityService.GetUserByIdAsync(request.UserId!);

            if (getUserByIdResult.IsError)
            {
                _logger.LogError("User with Id { UserId }{ErrorDetails}", request.UserId, getUserByIdResult.TopError.Message);

                return getUserByIdResult.Errors;
            }

            return getUserByIdResult.Value;
        }
    }
}
