using Microsoft.IdentityModel.Tokens;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoRabbitMQ.Services
{
    public class JwtTokenService(ILogger<JwtTokenService> logger) : ITokenService
    {
        private readonly string _privateKey = Environment.GetEnvironmentVariable("JWT_KEY") 
            ?? throw new ArgumentNullException(nameof(_privateKey));

        public Result<string> GenerateToken(string userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_privateKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Sha512);
            
            var claims = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
            ]);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Result<string>.Success(tokenHandler.WriteToken(token));
        }

        public Result<bool> IsValidToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_privateKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(token, validationParameters, out _);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Invalid Token: {ErrorMessage}", ex.Message);
            }
        }
    }
}
