using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Dto;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Commands
{
    public record RequestMangaOrderCommand(int CustomerId, IEnumerable<MangaOrderItemDto> OrderedMangas) : IRequest<Result<RequestedMangaOrderResponse>>;
}
