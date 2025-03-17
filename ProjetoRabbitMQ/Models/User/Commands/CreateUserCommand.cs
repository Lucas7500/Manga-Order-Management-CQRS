using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public record CreateUserCommand(
        string Name, 
        string Email, 
        string Password, 
        UserRole Role) : IRequest<Result<CreatedUserResponse>>;
}
