using MediatR;
using ProjetoRabbitMQ.Models.Results;

namespace ProjetoRabbitMQ.Models.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResult>>;
}
