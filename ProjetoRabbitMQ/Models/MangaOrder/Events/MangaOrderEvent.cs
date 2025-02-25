namespace ProjetoRabbitMQ.Models.MangaOrder.Events
{
    public record MangaOrderEvent(Ulid MangaOrderId, CancellationToken StoppingToken);
}
