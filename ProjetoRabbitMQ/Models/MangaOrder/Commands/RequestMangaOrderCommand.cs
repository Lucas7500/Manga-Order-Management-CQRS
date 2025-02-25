using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Commands
{
    public class RequestMangaOrderCommand : IRequest<Result<RequestedMangaOrderResponse>>
    {
    }
}
