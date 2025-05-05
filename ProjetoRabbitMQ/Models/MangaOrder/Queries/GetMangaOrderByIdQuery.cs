using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.MangaOrder.Queries
{
    public record GetMangaOrderByIdQuery(Ulid MangaOrderId) : IRequest<Result<MangaOrderEntity>>;
}
