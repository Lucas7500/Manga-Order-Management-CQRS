using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreatedUserResponse>>
    {
        public Task<Result<CreatedUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
