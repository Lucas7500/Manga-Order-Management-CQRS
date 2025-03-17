using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.User;
using Scrypt;

namespace ProjetoRabbitMQ.Infrastructure.Configuration
{
    internal static class MigrationData
    {
        internal static readonly UserEntity AdminUser = new()
        {
            Id = -1,
            Name = "admin",
            Email = string.Empty,
            Role = UserRole.Admin,
            PasswordHash = "admin",
        };
    }
}
