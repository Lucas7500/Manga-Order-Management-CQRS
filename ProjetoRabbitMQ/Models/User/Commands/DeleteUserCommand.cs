using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>;
}
