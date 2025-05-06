using FluentValidation;
using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Models.User.Responses;
using ProjetoRabbitMQ.Services.Interfaces;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        IValidator<CreateUserCommand> validator,
        IUnitOfWork unitOfWork,
        IPasswordHasher hasher) : IRequestHandler<CreateUserCommand, Result<CreatedUserResponse>>
    {
        public async Task<Result<CreatedUserResponse>> Handle(CreateUserCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Creation of User with email: {Email}", request.Email);

            var validationResult = await validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                return Result<CreatedUserResponse>.Failure(validationResult.ToString());
            }

            var repository = unitOfWork.UserRepository;
            if (await repository.HasAnyAsync(user => user.Email == request.Email, ct))
            {
                return Result<CreatedUserResponse>.Failure("User with this email already exists!");
            }

            var hashedPasswordResult = hasher.Hash(request.Password);
            if (hashedPasswordResult.IsFailure)
            {
                return Result<CreatedUserResponse>.Failure("Error hashing password!");
            }

            var user = new UserEntity
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashedPasswordResult.Value,
                Role = request.Role
            };

            await repository.AddAsync(user, ct);
            await unitOfWork.CommitAsync(ct);

            return Result<CreatedUserResponse>.Success(new CreatedUserResponse(user.Id));
        }
    }
}
