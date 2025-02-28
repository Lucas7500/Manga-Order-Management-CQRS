using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface ITokenService
    {
        Result<string> GenerateToken(int userId, string email);

        Task<Result<bool>> IsValidToken(string token);
    }
}
