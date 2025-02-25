using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public class DeleteMangaCommand : IRequest<Result<string>>
    {
    }
}
