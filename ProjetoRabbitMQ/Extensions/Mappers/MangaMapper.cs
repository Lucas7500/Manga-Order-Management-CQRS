using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.Manga.Responses;
using ProjetoRabbitMQ.Models.Manga;

namespace ProjetoRabbitMQ.Extensions.Mappers
{
    public static class MangaMapper
    {
        public static IEnumerable<MangaQueryModel> ToQueryModel(this IEnumerable<MangaEntity> mangas)
        {
            return mangas.Select(manga => manga.ToQueryModel());
        }

        public static MangaQueryModel ToQueryModel(this MangaEntity manga)
        {
            return new MangaQueryModel(
                manga.Title,
                manga.Author,
                manga.ReleaseDate,
                manga.Genres,
                manga.Aliases,
                manga.Quantity,
                manga.Price,
                manga.Description);
        }

        public static MangaEntity ToEntity(this MangaQueryModel queryModel)
        {
            return new MangaEntity
            {
                Title = queryModel.Title,
                Author = queryModel.Author,
                ReleaseDate = queryModel.ReleaseDate,
                Quantity = queryModel.Quantity,
                Price = queryModel.Price,
                Description = queryModel.Description,
                Genres = queryModel.Genres.ToList(),
                Aliases = queryModel.Aliases.ToList()
            };
        }

        public static MangaEntity ToEntity(this CreateMangaCommand command)
        {
            return new MangaEntity
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Author = command.Author,
                ReleaseDate = command.ReleaseDate,
                Quantity = command.Quantity,
                Price = command.Price,
                Description = command.Description,
                Genres = command.Genres.ToList(),
                Aliases = command.Aliases.ToList()
            };
        }
    }
}
