using FluentValidation;
using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Services.Interfaces;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class UpdateUserCommandHandler(
        ILogger<UpdateUserCommandHandler> logger,
        IValidator<UpdateUserCommand> validator,
        IUnitOfWork unitOfWork,
        IPasswordHasher hasher) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Update of User with Id: {Id}", request.UserId);

            var validationResult = await validator.ValidateAsync(request, ct);

            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.ToString());
            }

            var repository = unitOfWork.UserRepository;

            var user = await repository.GetAsync([request.UserId], ct);
            
            if (user == null)
            {
                return Result<string>.Failure("User not found!");
            }

            if (request.NewName != null)
            {
                user.Name = request.NewName;
            }

            if (request.NewEmail != null)
            {
                if (await repository.HasAnyAsync(user => user.Email == request.NewEmail, ct))
                {
                    return Result<string>.Failure("User with this email already exists!");
                }

                user.Email = request.NewEmail;
            }

            if (request.NewPassword != null)
            {
                if (request.CurrentPassword == null || !hasher.Compare(request.CurrentPassword, user.PasswordHash))
                {
                    return Result<string>.Failure("Incorrect Current Password!");
                }

                user.PasswordHash = hasher.Hash(request.NewPassword);
            }

            if (request.NewRole != null)
            {
                user.Role = request.NewRole.Value;
            }

            repository.Update(user);

            await unitOfWork.CommitAsync(ct);

            return Result<string>.Success(string.Empty);
        }
    }
}
