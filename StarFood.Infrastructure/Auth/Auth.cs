using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public string GenerateJwtToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua_chave_secreta"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "sua_issuer",
                audience: "sua_audiencia",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Token JWT gerada: {tokenString}");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}