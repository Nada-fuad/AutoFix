using System.Security.Claims;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Identity.Dtos;
using AutoFix.Application.Features.Identity.Queries.GenerateTokens;
using AutoFix.Application.Features.Identity.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(ISender sender, IIdentityService identityService, ITokenProvider tokenProvider) : ControllerBase
    {
        private readonly IIdentityService _identityService= identityService;
        private readonly ITokenProvider _tokenProvider=tokenProvider;
        
          
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(GenerateTokenQuery request, CancellationToken ct)
        {
            
            var result= await sender.Send(request, ct);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);

        }


        [HttpGet("current-user/claims")]
        [Authorize]
       [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
       [EndpointSummary("Gets the current authenticated user's info.")]
        [EndpointDescription("Returns user information for the currently authenticated user based on the access token.")]
        [EndpointName("GetCurrentUserClaims")]
        public async Task<IActionResult> GetCurrentUserInfo(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await sender.Send(new GetUserByIdQuery(userId), ct);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

    




    }
}
