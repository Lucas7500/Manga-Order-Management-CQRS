using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Login.Responses;

namespace ProjetoRabbitMQ.Models.Login.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;
}
