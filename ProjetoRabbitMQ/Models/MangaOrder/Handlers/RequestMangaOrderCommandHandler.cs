using FluentValidation;
using MassTransit;
using MediatR;
using ProjetoRabbitMQ.Bus.Events;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Responses;

namespace ProjetoRabbitMQ.Models.MangaOrder.Handlers
{
    public class RequestMangaOrderCommandHandler(
        ILogger<RequestMangaOrderCommandHandler> logger,
        IPublishEndpoint publishEndpoint,
        IValidator<RequestMangaOrderCommand> validator,
        IUnitOfWork unitOfWork) : IRequestHandler<RequestMangaOrderCommand, Result<RequestedMangaOrderResponse>>
    {
        public async Task<Result<RequestedMangaOrderResponse>> Handle(RequestMangaOrderCommand request, CancellationToken ct)
        {
            var repository = unitOfWork.MangaOrderRepository;

            var order = request.ToEntity();
            await repository.AddAsync(order, ct);
            
            var validationResult = await validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToString();

                logger.LogWarning("Manga Order request failed: {Errors}", errors);
                
                order.UpdatedAt = DateTime.UtcNow;
                order.Status = OrderStatus.Cancelled;
                order.CancellationReason = errors;

                await unitOfWork.CommitAsync(ct);
                return Result<RequestedMangaOrderResponse>.Failure(errors);
            }


            await unitOfWork.CommitAsync(ct);
            await publishEndpoint.Publish(new RequestedMangaOrderEvent(order.Id), ct);

            logger.LogInformation("Manga Order {OrderId} requested successfully", order.Id);

            return Result<RequestedMangaOrderResponse>.Success(new RequestedMangaOrderResponse(order.Id));
        }
    }
}
