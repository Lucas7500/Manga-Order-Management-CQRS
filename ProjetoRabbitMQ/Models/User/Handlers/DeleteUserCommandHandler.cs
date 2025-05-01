using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Commands;

namespace ProjetoRabbitMQ.Models.User.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}
