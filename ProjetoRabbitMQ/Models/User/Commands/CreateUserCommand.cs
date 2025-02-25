using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public class CreateUserCommand : IRequest<Result<CreatedUserResponse>>
    {
    }
}
