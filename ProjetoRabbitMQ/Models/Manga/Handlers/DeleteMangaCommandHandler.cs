using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.User.Handlers;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class DeleteMangaCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteMangaCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteMangaCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Deletion of Manga with id: {MangaId}", request.MangaId);

            var repository = unitOfWork.MangaRepository;

            var manga = await repository.GetAsync([request.MangaId], ct);
            if (manga == null)
            {
                return Result<bool>.Failure("Manga not found!");
            }

            repository.Delete(manga);

            await unitOfWork.CommitAsync(ct);

            return Result<bool>.Success(true);
        }
    }
}
