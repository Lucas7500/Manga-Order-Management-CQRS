using MediatR;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Queries;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class GetAllMangasQueryHandle(
        ILogger<GetAllMangasQueryHandle> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllMangasQuery, Result<List<MangaQueryModel>>>
    {
        public async Task<Result<List<MangaQueryModel>>> Handle(GetAllMangasQuery request, CancellationToken ct)
        {
            logger.LogInformation("Handling GetAllMangasQuery");

            var repository = unitOfWork.MangaRepository;

            var mangas = await repository.GetAllAsync(cancellationToken: ct);

            if (mangas is null or { Count: 0 })
            {
                return Result<List<MangaQueryModel>>.Failure("No mangas found!");
            }

            var mangaQueryModels = mangas.ToQueryModel().ToList();

            return Result<List<MangaQueryModel>>.Success(mangaQueryModels);
        }
    }
}
