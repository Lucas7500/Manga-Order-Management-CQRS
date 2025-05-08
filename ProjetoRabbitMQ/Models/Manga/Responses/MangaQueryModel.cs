namespace ProjetoRabbitMQ.Models.Manga.Responses
{
    public record MangaQueryModel(
            string Title,
            string Author,
            DateTime ReleaseDate,
            IEnumerable<string> Genres,
            IEnumerable<string> Aliases,
            uint Quantity,
            decimal Price,
            string? Description
        );
}
