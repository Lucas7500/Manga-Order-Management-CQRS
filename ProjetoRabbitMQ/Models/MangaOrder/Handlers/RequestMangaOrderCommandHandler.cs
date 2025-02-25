using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class RequestMangaOrderCommandHandler : IRequestHandler<RequestMangaOrderCommand, Result<RequestedMangaOrderResponse>>
    {
        public Task<Result<RequestedMangaOrderResponse>> Handle(RequestMangaOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
