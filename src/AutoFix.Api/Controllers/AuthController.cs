using AutoFix.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IIdentityService identityService,ITokenProvider tokenProvider)
        {
            _identityService = identityService;
            _tokenProvider = tokenProvider;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
        {
            var result = await _identityService.AuthenticateAsync(request.Email, request.Password);

            if (result.IsError)
            {
                return Unauthorized(result.Errors);
            }
            var tokenResult = await _tokenProvider.GenerateJwtTokenAsync(result.Value, ct);

            if (tokenResult.IsError)
            {
                return BadRequest(tokenResult.Errors);
            }

            return Ok(tokenResult.Value);

        }





    }
}
