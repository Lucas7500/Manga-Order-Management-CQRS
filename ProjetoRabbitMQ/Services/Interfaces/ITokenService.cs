using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface ITokenService
    {
        Result<string> GenerateToken(string userId, string email);

        Result<bool> IsValidToken(string token);
    }
}
