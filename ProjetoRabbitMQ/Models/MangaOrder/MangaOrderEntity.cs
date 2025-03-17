using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRabbitMQ.Models.MangaOrder
{
    [Table("orders")]
    public class MangaOrderEntity
    {
        public Ulid Id { get; init; }
        public int CustomerId { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public string? CancellationReason { get; set; }

        public UserEntity Customer { get; set; } = null!;
        public ICollection<MangaOrderItemEntity> OrderedMangas { get; set; } = null!;

        [NotMapped]
        public bool IsCancelled => Status == OrderStatus.Cancelled;
    }
}
