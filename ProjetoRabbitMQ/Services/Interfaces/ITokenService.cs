using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;

namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface ITokenService
    {
        Result<string> GenerateToken(int userId, string email, UserRole role);

        Task<Result<bool>> IsValidToken(string token);
    }
}
