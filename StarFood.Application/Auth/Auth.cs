using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StarFood.Application.DomainModel.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StarFood.Infrastructure.Auth
{
    public class Auth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(AuthCommand cmd)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StarFoodJSONWebTokenKey"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", cmd.UserId.ToString()),
                new Claim("email", cmd.Email),
                new Claim("role", cmd.Role),
                new Claim("name", cmd.Name),
                new Claim("restaurantId", cmd.RestaurantId.ToString()),
            };

            var expireAt = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7100",
                audience: "StarsFoodInc",
                claims: claims,
                expires: expireAt,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}