using FluentValidation;
using MediatR;
using ProjetoRabbitMQ.Infrastructure.Interfaces;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Manga.Commands;

namespace ProjetoRabbitMQ.Models.Manga.Handlers
{
    public class UpdateMangaCommandHandler(
        ILogger<UpdateMangaCommandHandler> logger,
        IValidator<UpdateMangaCommand> validator,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateMangaCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateMangaCommand request, CancellationToken ct)
        {
            logger.LogInformation("Handling Update of Manga with Id: {MangaId}", request.MangaId);

            var validationResult = await ValidateUpdateRequest(request, ct);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            var repository = unitOfWork.MangaRepository;
            var manga = await repository.GetAsync([request.MangaId], ct);

            if (manga == null)
            {
                return Result<bool>.Failure("Manga not found!");
            }

            var updateResult = await UpdateMangaProperties(repository, request, manga, ct);
            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            repository.Update(manga);
            await unitOfWork.CommitAsync(ct);

            return Result<bool>.Success(true);
        }

        private async Task<Result<bool>> ValidateUpdateRequest(UpdateMangaCommand request, CancellationToken ct)
        {
            var validationResult = await validator.ValidateAsync(request, ct);

            return validationResult.IsValid
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(validationResult.ToString());
        }

        private async Task<Result<bool>> UpdateMangaProperties(
            IRepository<MangaEntity> repository,
            UpdateMangaCommand request,
            MangaEntity manga,
            CancellationToken ct)
        {
            if (request.Title != null && request.Title != manga.Title)
            {
                var mangaInDb = await repository.GetAsync(manga =>
                    (manga.Title == request.Title || manga.Aliases.Contains(request.Title))
                    && manga.Author == request.Author, ct);

                if (mangaInDb != null)
                {
                    return Result<bool>.Failure("This manga is already registered as {MangaName}!", mangaInDb.Title);
                }

                manga.Title = request.Title;
            }

            if (request.Description != null && request.Description != manga.Description)
            {
                manga.Description = request.Description;
            }

            if (request.Author != null && request.Author != manga.Author)
            {
                manga.Author = request.Author;
            }

            if (request.ReleaseDate.HasValue && request.ReleaseDate != manga.ReleaseDate)
            {
                manga.ReleaseDate = request.ReleaseDate.Value;
            }

            if (request.Quantity.HasValue && request.Quantity != manga.Quantity)
            {
                manga.Quantity = request.Quantity.Value;
            }

            if (request.Price.HasValue && request.Price != manga.Price)
            {
                manga.Price = request.Price.Value;
            }

            if (request.Aliases != null)
            {
                manga.Aliases = request.Aliases.Distinct().ToList();
            }

            if (request.Genres != null)
            {
                manga.Genres = request.Genres.Distinct().ToList();
            }

            return Result<bool>.Success(true);
        }
    }
}
