using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public record UpdateUserCommand(
        int UserId,
        string? NewName,
        string? NewEmail,
        string? NewPassword,
        string? CurrentPassword,
        UserRole? NewRole) : IRequest<Result<bool>>;
}
