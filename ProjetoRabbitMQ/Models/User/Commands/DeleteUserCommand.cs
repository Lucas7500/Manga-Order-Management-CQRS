using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
    }
}
