using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Queries
{
    public record GetUserByIdQuery(int UserId) : IRequest<Result<UserQueryModel>>;
}
