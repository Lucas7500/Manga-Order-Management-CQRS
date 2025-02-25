using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Queries
{
    public class GetAllMangaOrderQuery : IRequest<Result<List<MangaOrderQueryModel>>>
    {
    }
}
