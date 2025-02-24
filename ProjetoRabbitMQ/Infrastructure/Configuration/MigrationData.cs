using ProjetoRabbitMQ.Models;
using ProjetoRabbitMQ.Models.Enums;
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
            Salt = null
        };
    }
}
