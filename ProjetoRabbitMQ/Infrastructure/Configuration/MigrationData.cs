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
            Email = "admin@admin.adm",
            Role = UserRole.Admin,
            PasswordHash = "$s2$16384$8$1$xvgcEfj1T0uZwg2gafN3AvMPLO5sW6dUQaz+mLZHSps=$ju73zxXgu81QJP132+M8590VbXA3w7CL1csGnlJ05/8=",
        };
    }
}
