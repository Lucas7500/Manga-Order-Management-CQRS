using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;

namespace ProjetoRabbitMQ.Models.User.Commands
{
    public record UpdateUserCommand : IRequest<Result<string>>
    {
        public Guid UserId { get; set; }
        public string? NewName { get; init; }
        public string? NewEmail { get; init; }
        public string? NewPassword { get; init; }
        public string? CurrentPassword { get; init; }
        public UserRole? NewRole { get; init; }
    }
}
