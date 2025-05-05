using ProjetoRabbitMQ.Models.Enums;

namespace ProjetoRabbitMQ.Models.MangaOrder.Responses
{
    public record MangaOrderQueryModel(
            Ulid Id,
            DateTime OrderDate,
            IEnumerable<Guid> OrderedMangas,
            decimal TotalPrice,
            OrderStatus Status,
            string? CancellationReason
        );
}
