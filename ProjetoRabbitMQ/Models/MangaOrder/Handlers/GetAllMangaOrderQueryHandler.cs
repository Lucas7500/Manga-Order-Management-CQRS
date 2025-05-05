using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class GetAllMangaOrderQueryHandler(
        ILogger<GetAllMangaOrderQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllMangaOrderQuery, Result<List<MangaOrderQueryModel>>>
    {
        public async Task<Result<List<MangaOrderQueryModel>>> Handle(GetAllMangaOrderQuery request, CancellationToken ct)
        {
            logger.LogInformation("Handling GetAllMangaOrderQuery for CustomerId: {CustomerId}", request.CustomerId);

            var repository = unitOfWork.MangaOrderRepository;

            var orders = await repository
                .IncludeForNextQuery(order => order.OrderedMangas)
                .GetAllAsync(order => order.CustomerId == request.CustomerId, ct);

            if (orders is null or { Count: 0 })
            {
                return Result<List<MangaOrderQueryModel>>.Failure("No manga orders found for the given customer.");
            }

            var orderedMangasInfo = orders
                .SelectMany(order => order.OrderedMangas)
                .Aggregate(new OrderedMangasResult([], 0m),
                    (accumulator, orderedManga) =>
                    {
                        accumulator.Ids.Add(orderedManga.Id);
                        var totalPrice = accumulator.TotalPrice + orderedManga.TotalPrice;

                        return new OrderedMangasResult(accumulator.Ids, totalPrice);
                    });

            var ordersDto = orders
                .Select(mangaOrder => new MangaOrderQueryModel(
                    mangaOrder.Id,
                    mangaOrder.CreatedAt,
                    orderedMangasInfo.Ids,
                    orderedMangasInfo.TotalPrice,
                    mangaOrder.Status,
                    mangaOrder.CancellationReason))
                .ToList();

            return Result<List<MangaOrderQueryModel>>.Success(ordersDto);
        }

        private record OrderedMangasResult(List<Guid> Ids, decimal TotalPrice);
    }
}
