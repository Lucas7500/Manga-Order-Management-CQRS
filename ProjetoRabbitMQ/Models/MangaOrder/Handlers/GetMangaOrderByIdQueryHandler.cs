using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class GetMangaOrderByIdQueryHandler(
        ILogger<GetMangaOrderByIdQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetMangaOrderByIdQuery, Result<MangaOrderEntity>>
    {
        public async Task<Result<MangaOrderEntity>> Handle(GetMangaOrderByIdQuery request, CancellationToken ct)
        {
            logger.LogInformation("Handling GetMangaOrderByIdQuery for MangaOrderId: {MangaOrderId}", request.MangaOrderId);

            var repository = unitOfWork.MangaOrderRepository;

            var order = await repository
                .IncludeForNextQuery(order => order.OrderedMangas)
                .GetAsync(order => order.Id == request.MangaOrderId, ct);

            return order is null
                ? Result<MangaOrderEntity>.Failure("No manga order found for the given Id.")
                : Result<MangaOrderEntity>.Success(order);
        }
    }
}
