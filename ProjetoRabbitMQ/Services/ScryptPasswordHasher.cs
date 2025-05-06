using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Services.Interfaces;
using Scrypt;

namespace ProjetoRabbitMQ.Services
{
    public class ScryptPasswordHasher(ILogger<ScryptPasswordHasher> logger, ScryptEncoder encoder) : IPasswordHasher
    {
        public Result<string> Hash(string password)
        {
            logger.LogInformation("Started hashing password");

            try
            {
                var hashedPassword = encoder.Encode(password);
                logger.LogInformation("Password hashed successfully");
                return Result<string>.Success(hashedPassword);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error hashing password");
                return Result<string>.Failure(ex.Message);
            }
        }

        public Result<bool> Compare(string password, string hashedPassword)
        {
            logger.LogInformation("Started comparing password");

            try
            {
                var isMatch = encoder.Compare(password, hashedPassword);
                logger.LogInformation("Password comparison completed");
                return Result<bool>.Success(isMatch);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error comparing password");
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
