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
        IPasswordHasher hasher) : IRequestHandler<UpdateUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Update of User with Id: {Id}", request.UserId);

            var validationResult = await ValidateUpdateRequest(request, ct);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var repository = unitOfWork.UserRepository;
            var user = await repository.GetAsync([request.UserId], ct);
            
            if (user == null)
            {
                return Result<bool>.Failure("User not found!");
            }

            var updateResult = await UpdateUserProperties(repository, request, user, ct);
            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            repository.Update(user);
            await unitOfWork.CommitAsync(ct);

            return Result<bool>.Success(true);
        }

        private async Task<Result<bool>> ValidateUpdateRequest(UpdateUserCommand request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request, ct);

            return validationResult.IsValid
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(validationResult.ToString());
        }

        private async Task<Result<bool>> UpdateUserProperties(
            IRepository<UserEntity> repository, 
            UpdateUserCommand request, 
            UserEntity user,
            CancellationToken ct)
        {
            if (!string.IsNullOrWhiteSpace(request.NewName))
            {
                user.Name = request.NewName;
            }

            if (!string.IsNullOrWhiteSpace(request.NewEmail))
            {
                var isDuplicateEmail = await repository.HasAnyAsync(user => user.Email == request.NewEmail, ct);
                
                if (isDuplicateEmail)
                {
                    return Result<bool>.Failure("User with this email already exists!");
                }

                user.Email = request.NewEmail;
            }

            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                if (request.CurrentPassword == null)
                {
                    return Result<bool>.Failure("Current Password must be passed!");
                }

                var isCurrentPasswordValidResult = hasher.Compare(request.CurrentPassword, user.PasswordHash);
                if (isCurrentPasswordValidResult.IsFailure)
                {
                    return Result<bool>.Failure("Error comparing passwords: {ErrorMessage}", isCurrentPasswordValidResult.ErrorMessage);
                }

                if (!isCurrentPasswordValidResult.Value)
                {
                    return Result<bool>.Failure("Current Password is incorrect!");
                }

                var hashedPasswordResult = hasher.Hash(request.NewPassword);
                if (hashedPasswordResult.IsFailure)
                {
                    return Result<bool>.Failure("Error hashing password: {ErrorMessage}", hashedPasswordResult.ErrorMessage);
                }

                user.PasswordHash = hashedPasswordResult.Value;
            }

            if (request.NewRole.HasValue)
            {
                user.Role = request.NewRole.Value;
            }

            return Result<bool>.Success(true);
        }
    }
}
