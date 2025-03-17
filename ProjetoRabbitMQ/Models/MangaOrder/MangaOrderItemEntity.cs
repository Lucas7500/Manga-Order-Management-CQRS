using ProjetoRabbitMQ.Models.Manga;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.MangaOrder
{
    [Table("ordered_items")]
    public class MangaOrderItemEntity
    {
        public Guid Id { get; set; }
        public Guid MangaId { get; set; }
        public int Quantity { get; set; }

        public MangaOrderEntity Order { get; set; } = null!;
        public MangaEntity OrderedManga { get; set; } = null!;

        [NotMapped]
        public decimal TotalPrice => OrderedManga.Price * Quantity;
    }
}
