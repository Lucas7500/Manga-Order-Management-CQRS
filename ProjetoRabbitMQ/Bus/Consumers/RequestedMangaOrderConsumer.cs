using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProjetoRabbitMQ.Bus.Events;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.Joins;
using ProjetoRabbitMQ.Models.MangaOrder;

namespace ProjetoRabbitMQ.Bus.Consumers
{
    public class RequestedMangaOrderConsumer(
        ILogger<RequestedMangaOrderConsumer> logger,
        IUnitOfWork unitOfWork) : IConsumer<RequestedMangaOrderEvent>
    {
        public async Task Consume(ConsumeContext<RequestedMangaOrderEvent> context)
        {
            logger.LogInformation("Processing Manga Order Id: {Id}", context.Message.MangaOrderId);

            var mangaOrder = await unitOfWork
                .MangaOrderRepository
                .DbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderedMangas)
                    .ThenInclude(om => om.OrderedManga)
                .AsSplitQuery()
                .FirstOrDefaultAsync(o => o.Id == context.Message.MangaOrderId, context.CancellationToken);

            if (mangaOrder is null)
            {
                logger.LogWarning("Manga Order Id: {Id} not found", context.Message.MangaOrderId);
                return;
            }

            if (mangaOrder.IsCancelled)
            {
                logger.LogWarning("Manga Order Id: {Id} is cancelled", context.Message.MangaOrderId);
                return;
            }

            mangaOrder.UpdatedAt = DateTime.UtcNow;
            mangaOrder.Status = OrderStatus.Processing;
            await unitOfWork.CommitAsync(context.CancellationToken);

            logger.LogInformation("Manga Order Id: {Id} is now being processed", context.Message.MangaOrderId);

            var handleOrderResult = await HandleOrder(mangaOrder, context.CancellationToken);
            
            if (handleOrderResult.IsFailure)
            {
                logger.LogWarning("Manga Order Id: {Id} failed to process", context.Message.MangaOrderId);
                
                mangaOrder.Status = OrderStatus.Cancelled;
                mangaOrder.CancellationReason = handleOrderResult.ErrorMessage;

                await unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }
            
            mangaOrder.Status = OrderStatus.Completed;
            mangaOrder.UpdatedAt = DateTime.UtcNow;
            await unitOfWork.CommitAsync(context.CancellationToken);

            logger.LogInformation("Manga Order Id: {Id} processed successfully", context.Message.MangaOrderId);
        }

        private async Task<Result<bool>> HandleOrder(MangaOrderEntity mangaOrder, CancellationToken ct)
        {
            foreach (var order in mangaOrder.OrderedMangas)
            {
                var orderedManga = order.OrderedManga;

                if (order.Quantity > orderedManga.Quantity)
                {
                    logger.LogWarning("Manga Id: {Id} has insufficient quantity", order.MangaId);

                    return Result<bool>.Failure($"Insufficient quantity of '{orderedManga.Title}'!");
                }

                orderedManga.Quantity -= (uint)order.Quantity;

                await unitOfWork.UserMangaRepository.AddAsync(new UserMangaEntity
                {
                    UserId = mangaOrder.CustomerId,
                    MangaId = order.MangaId,
                }, ct);
            }

            return Result<bool>.Success(true);
        }
    }
}
