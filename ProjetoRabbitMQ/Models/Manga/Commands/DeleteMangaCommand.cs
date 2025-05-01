using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public record DeleteMangaCommand(Guid MangaId) : IRequest<Result<bool>>;
}
