using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.User;
using Scrypt;

namespace ProjetoRabbitMQ.Infrastructure.Configuration
{
    internal static class MigrationData
    {
        internal static readonly User AdminUser = new()
        {
            Name = "admin",
            Email = string.Empty,
            Role = UserRole.Admin,
            PasswordHash = new ScryptEncoder().Encode(Environment.GetEnvironmentVariable("ADMIN_PASSWORD")),
        };
    }
}
