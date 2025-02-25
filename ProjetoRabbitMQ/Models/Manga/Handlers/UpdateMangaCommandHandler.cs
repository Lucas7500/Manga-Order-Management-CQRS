using MediatR;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class UpdateMangaCommandHandler : IRequestHandler<UpdateMangaCommand, Result<string>>
    {
        public Task<Result<string>> Handle(UpdateMangaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
