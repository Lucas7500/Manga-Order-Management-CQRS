using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;
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

        public Result<string> GenerateToken(int userId, string email, UserRole role)
        {
            logger.LogInformation("Started generating JWT token");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_privateKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512);

            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role.ToString())
            };

            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(1),
            });

            return Result<string>.Success(tokenHandler.WriteToken(token));
        }

        public async Task<Result<bool>> IsValidToken(string token)
        {
            try
            {
                logger.LogInformation("Started validating JWT token");

                var tokenHandler = new JwtSecurityTokenHandler();
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

                var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);

                logger.LogInformation("Finished JWT token validation");

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                logger.LogError("Error with JWT token validation: {Exception}", ex.ToString());

                return Result<bool>.Failure("Invalid Token: {ErrorMessage}", ex.Message);
            }
        }
    }
}
