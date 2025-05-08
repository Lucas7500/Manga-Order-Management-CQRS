using MediatR;
using ProjetoRabbitMQ.Extensions.Mappers;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Queries;
using ProjetoRabbitMQ.Models.Manga.Responses;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class GetMangaByIdQueryHandler(
        ILogger<GetMangaByIdQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetMangaByIdQuery, Result<MangaQueryModel>>
    {
        public async Task<Result<MangaQueryModel>> Handle(GetMangaByIdQuery request, CancellationToken ct)
        {
           logger.LogInformation("Handling GetMangaByIdQuery for Manga with id: {MangaId}", request.MangaId);

            var repository = unitOfWork.MangaRepository;

            var manga = await repository.GetAsync([request.MangaId], ct);
            
            if (manga is null)
            {
                return Result<MangaQueryModel>.Failure("Manga not found!");
            }

            var mangaQueryModel = manga.ToQueryModel();
            
            return Result<MangaQueryModel>.Success(mangaQueryModel);
        }
    }
}
