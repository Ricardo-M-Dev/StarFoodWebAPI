using Microsoft.AspNetCore.Mvc;
using StarFood.Infrastructure.Auth;

namespace StarsFoodAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Auth _auth;

        public AuthController(Auth auth)
        {
            _auth = auth;
        }

        [HttpPost("Login")]
        public IActionResult Login()
        {
            var userId = "1";
            var token = _auth.GenerateJwtToken(userId);

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