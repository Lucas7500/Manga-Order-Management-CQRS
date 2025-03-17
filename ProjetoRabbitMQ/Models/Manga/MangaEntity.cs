using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Joins;
using ProjetoRabbitMQ.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.Manga
{
    [Table("mangas")]
    public class MangaEntity : Product
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public List<string> Genres { get; set; } = [];
        public List<string> Aliases { get; set; } = [];

        public ICollection<UserEntity> Users { get; set; } = null!;
        public ICollection<UserMangaEntity> UserMangas { get; set; } = null!;
    }
}