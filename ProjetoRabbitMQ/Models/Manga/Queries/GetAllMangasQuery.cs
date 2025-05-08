using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Queries
{
    public record GetAllMangasQuery() : IRequest<Result<List<MangaQueryModel>>>;
}
