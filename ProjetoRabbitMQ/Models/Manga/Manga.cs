using ProjetoRabbitMQ.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.Manga
{
    [Table("mangas")]
    public class Manga : Product
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public List<string> Genres { get; set; } = [];
        public List<string> Aliases { get; set; } = [];
    }
}