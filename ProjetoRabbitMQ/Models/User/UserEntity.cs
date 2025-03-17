using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.Joins;
using ProjetoRabbitMQ.Models.MangaOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.User
{
    [Table("users")]
    public class UserEntity
    {
        public int Id { get; init; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public ICollection<MangaOrderEntity> Orders { get; set; } = null!;
        public ICollection<UserMangaEntity> UserMangas { get; set; } = null!;
    }
}