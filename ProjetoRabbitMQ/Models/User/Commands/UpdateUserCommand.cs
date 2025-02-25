using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public class UpdateUserCommand : IRequest<Result<string>>
    {
    }
}
