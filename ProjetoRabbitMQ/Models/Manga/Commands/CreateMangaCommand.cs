using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public class CreateMangaCommand : IRequest<Result<CreatedMangaResult>>
    {
    }
}
