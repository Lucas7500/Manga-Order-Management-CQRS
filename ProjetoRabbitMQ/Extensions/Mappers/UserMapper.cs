using ProjetoRabbitMQ.Models.User;
using ProjetoRabbitMQ.Models.User.Commands;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Extensions.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this CreateUserCommand command, string hashedPassword)
        {
            return new UserEntity
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = hashedPassword,
                Role = command.Role
            };
        }

        public static IEnumerable<UserQueryModel> ToQueryModel(this IEnumerable<UserEntity> users)
        {
            return users.Select(user => user.ToQueryModel());
        }

        public static UserQueryModel ToQueryModel(this UserEntity user)
        {
            return new UserQueryModel(
                user.Id,
                user.Name,
                user.Email,
                user.Role);
        }
    }
}
