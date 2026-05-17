using System.Security.Claims;
using AutoFix.Application.Common.Interfaces;

namespace AutoFix.Api.services
{
    public class CurrentUser:IUser

    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public string? Id => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
