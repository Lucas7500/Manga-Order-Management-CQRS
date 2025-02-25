using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Queries
{
    public class GetMangaOrderByIdQuery : IRequest<Result<MangaOrderQueryModel>>
    {
    }
}
