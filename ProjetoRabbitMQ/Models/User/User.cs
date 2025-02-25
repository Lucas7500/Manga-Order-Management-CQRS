using ProjetoRabbitMQ.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.User
{
    [Table("users")]
    public class User
    {
        public int Id { get; init; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public byte[]? Salt { get; set; }
        public UserRole Role { get; init; }
    }
}
