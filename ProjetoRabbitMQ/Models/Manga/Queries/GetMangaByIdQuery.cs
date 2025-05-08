using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Queries
{
    public record GetMangaByIdQuery(Guid MangaId) : IRequest<Result<MangaQueryModel>;
}
