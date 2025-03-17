using ProjetoRabbitMQ.Models.Manga;
using ProjetoRabbitMQ.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.Joins
{
    [Table("user_mangas")]
    public class UserMangaEntity
    {
        public int UserId { get; set; }
        public Guid MangaId { get; set; }

        public UserEntity User { get; set; } = null!;
        public MangaEntity Manga { get; set; } = null!;
    }
}
