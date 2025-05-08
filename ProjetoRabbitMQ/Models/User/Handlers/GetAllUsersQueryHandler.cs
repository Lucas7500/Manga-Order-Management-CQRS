using MediatR;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Queries;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class GetAllUsersQueryHandler(
        ILogger<GetAllUsersQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersQuery, Result<List<UserQueryModel>>>
    {
        public async Task<Result<List<UserQueryModel>>> Handle(GetAllUsersQuery request, CancellationToken ct)
        {
            logger.LogInformation("Handling GetAllUsersQuery");

            var repository = unitOfWork.UserRepository;

            var users = await repository.GetAllAsync(cancellationToken: ct);

            if (users is null or { Count: 0 })
            {
                return Result<List<UserQueryModel>>.Failure("No users found!");
            }

            var userQueryModels = users.ToQueryModel().ToList();

            return Result<List<UserQueryModel>>.Success(userQueryModels);
        }
    }
}
