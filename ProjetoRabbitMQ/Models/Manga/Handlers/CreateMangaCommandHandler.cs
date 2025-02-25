using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class CreateMangaCommandHandler : IRequestHandler<CreateMangaCommand, Result<CreatedMangaResult>>
    {
        public Task<Result<CreatedMangaResult>> Handle(CreateMangaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
