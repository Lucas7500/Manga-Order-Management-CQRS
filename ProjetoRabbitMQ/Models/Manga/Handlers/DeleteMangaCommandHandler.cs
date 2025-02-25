using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class DeleteMangaCommandHandler : IRequestHandler<DeleteMangaCommand, Result<string>>
    {
        public Task<Result<string>> Handle(DeleteMangaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
