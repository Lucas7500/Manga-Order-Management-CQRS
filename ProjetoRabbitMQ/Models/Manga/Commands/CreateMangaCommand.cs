using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public record CreateMangaCommand(
        string Title,
        string Author,
        DateTime ReleaseDate,
        uint Quantity,
        decimal Price,
        string? Description,
        List<string> Genres,
        List<string> Aliases) : IRequest<Result<CreatedMangaResult>>;
}
