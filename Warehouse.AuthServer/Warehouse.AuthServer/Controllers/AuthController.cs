using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Services;
using Warehouse.AuthServer.Services.Jwts;

namespace Warehouse.AuthServer.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost()]
        public async Task<IActionResult> Login(Login user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existsUser = await authService.Login(user);
            if (existsUser)
            {
                var tokenString = authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }
    }
}
