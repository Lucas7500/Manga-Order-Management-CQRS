using ProjetoRabbitMQ.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.MangaOrder
{
    [Table("orders")]
    public class MangaOrder
    {
        public Ulid Id { get; init; }
        public int CustomerId { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public string? CancellationReason { get; set; }
        public List<MangaOrderItem> Mangas { get; init; } = [];

        [NotMapped]
        public bool IsCancelled => Status == OrderStatus.Cancelled;

        public record MangaOrderItem(Guid MangaId, int Quantity);
    }
}
