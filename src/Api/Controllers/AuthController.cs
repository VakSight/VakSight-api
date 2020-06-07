using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaceses;

namespace Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public IActionResult Token(string username, string password)
        {
            string token = authService.BuildToken(username, password);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
        
        
    }
}