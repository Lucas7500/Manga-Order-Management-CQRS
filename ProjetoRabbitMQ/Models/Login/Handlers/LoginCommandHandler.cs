using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Login.Commands;
using ProjetoRabbitMQ.Models.Login.Responses;
using ProjetoRabbitMQ.Services.Interfaces;

namespace ProjetoRabbitMQ.Models.Login.Handlers
{
    public class LoginCommandHandler(
        ILogger<LoginCommandHandler> logger, 
        IUnitOfWork unitOfWork, 
        IPasswordHasher hasher,
        ITokenService tokenService) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Login of User with email: {Email}", request.Email);

            var user = await unitOfWork.UserRepository.GetAsync(user => user.Email == request.Email, ct);

            if (user == null)
            {
                return Result<LoginResponse>.Failure("Incorrect User Email or Password!");
            }

            var hashedPasswordResult = hasher.Compare(request.Password, user.PasswordHash);

            if (hashedPasswordResult.IsFailure)
            {
                return Result<LoginResponse>.Failure("Error When Comparing Passwords: {ErrorMessage}", hashedPasswordResult.ErrorMessage);
            }

            if (!hashedPasswordResult.Value)
            {
                return Result<LoginResponse>.Failure("Incorrect User Email or Password!");
            }

            var tokenResult = tokenService.GenerateToken(user.Id, user.Email);
            if (tokenResult.IsFailure)
            {
                return Result<LoginResponse>.Failure("Failed in Token Generation!");
            }

            return Result<LoginResponse>.Success(new LoginResponse(tokenResult.Value));
        }
    }
}
