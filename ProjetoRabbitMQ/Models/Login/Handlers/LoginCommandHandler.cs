using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Login.Commands;
using ProjetoRabbitMQ.Models.Login.Responses;

namespace ProjetoRabbitMQ.Models.Login.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        public Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
        {
            var token = "";
            var result = new LoginResponse(token);

            return Task.FromResult(Result<LoginResponse>.Success(result));
        }
    }
}
