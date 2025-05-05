using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Queries
{
    public record GetAllMangaOrderQuery(int CustomerId) : IRequest<Result<List<MangaOrderQueryModel>>>;
}
