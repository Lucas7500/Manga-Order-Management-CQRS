using MediatR;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Queries;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class GetUserByIdQueryHandler(
        ILogger<GetUserByIdQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, Result<UserQueryModel>>
    {
        public async Task<Result<UserQueryModel>> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            logger.LogInformation("Handling GetUserByIdQuery for User with id: {UserId}", request.UserId);

            var repository = unitOfWork.UserRepository;

            var user = await repository.GetAsync([request.UserId], ct);
            
            if (user is null)
            {
                return Result<UserQueryModel>.Failure("User not found.");
            }
            var userQueryModel = user.ToQueryModel();

            return Result<UserQueryModel>.Success(userQueryModel);
        }
    }
}
