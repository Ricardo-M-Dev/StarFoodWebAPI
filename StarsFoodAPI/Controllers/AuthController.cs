using Microsoft.AspNetCore.Mvc;
using StarFood.Application.DomainModel.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Auth;
using StarsFoodAPI.Services.HttpContext;

namespace StarsFoodAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Auth _auth;
        private readonly RequestState _authenticatedContext;

        public AuthController(Auth auth, RequestState authenticatedContext)
        {
            _auth = auth;
            _authenticatedContext = authenticatedContext;
        }

        [HttpPost("Login")]
        public IActionResult Login(
        [FromBody] AuthCommand cmd
        )
        {
            var token = _auth.GenerateJwtToken(cmd);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = Request.IsHttps,
                Expires = DateTime.Now.AddHours(1)
            };

            Response.Cookies.Append("JwtToken", token, cookieOptions);
            return Ok(new { Token = token });
        }
    }
}