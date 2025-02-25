using MediatR;
using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Models.Manga.Commands
{
    public class UpdateMangaCommand : IRequest<Result<string>>
    {
    }
}
