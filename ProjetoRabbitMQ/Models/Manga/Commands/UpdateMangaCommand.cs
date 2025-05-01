using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public record UpdateMangaCommand(
        Guid MangaId,
        string? Title,
        string? Author,
        DateTime? ReleaseDate,
        uint? Quantity,
        decimal? Price,
        string? Description,
        List<string>? Genres,
        List<string>? Aliases) : IRequest<Result<bool>>;
}
