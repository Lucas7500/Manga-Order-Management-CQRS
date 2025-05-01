using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public record DeleteUserCommand(int UserId) : IRequest<Result<bool>>;
}
