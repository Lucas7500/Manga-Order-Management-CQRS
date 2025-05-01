using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Services.Interfaces;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IPasswordHasher hasher) : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Creation of User with id: {UserId}", request.UserId);

            var repository = unitOfWork.UserRepository;

            var user = await repository.GetAsync([request.UserId], ct);
            if (user == null)
            {
                return Result<bool>.Failure("User not found!");
            }

            repository.Delete(user);

            await unitOfWork.CommitAsync(ct);

            return Result<bool>.Success(true);
        }
    }
}
