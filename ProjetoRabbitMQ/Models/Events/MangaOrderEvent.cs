namespace ProjetoRabbitMQ.Models.Events
{
    public record MangaOrderEvent(Ulid MangaOrderId, CancellationToken StoppingToken);
}
