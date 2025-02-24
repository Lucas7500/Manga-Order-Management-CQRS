using MediatR;
using ProjetoRabbitMQ.Models.Commands;
using ProjetoRabbitMQ.Models.Results;

namespace ProjetoRabbitMQ.Models.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResult>>
    {
        public Task<Result<LoginResult>> Handle(LoginCommand request, CancellationToken ct)
        {
            var token = "";
            var result = new LoginResult(token);

            return Task.FromResult(Result<LoginResult>.Success(result));
        }
    }
}
