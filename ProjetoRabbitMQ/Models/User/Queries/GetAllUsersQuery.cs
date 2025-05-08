using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.User.Responses;

namespace ProjetoRabbitMQ.Models.User.Queries
{
    public record GetAllUsersQuery() : IRequest<Result<List<UserQueryModel>>>;
}
