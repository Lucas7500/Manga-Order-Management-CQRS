using ProjetoRabbitMQ.Models.Enums;

namespace ProjetoRabbitMQ.Models.User.Responses
{
    public record UserQueryModel(
        int Id,
        string Name,
        string Email,
        UserRole Role
    );
}
