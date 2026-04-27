using Microsoft.AspNetCore.Mvc;

namespace AutoFix.Api.Controllers.Auth
{
   
        public record LoginRequest(string Email, string Password);
    
}
