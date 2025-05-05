using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.Login.Commands;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;
using ProjetoRabbitMQ.Models.User.Commands;

namespace ProjetoRabbitMQ.Controllers
{
    public static class ApiEndpoints
    {
        public static void AddUsersEndpoints(this WebApplication app)
        {
            app.MapPost("users/login", 
                async (
                    [FromBody] LoginCommand command, 
                    [FromServices] IMediator mediator, 
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return result.IsSuccess
                        ? Results.Ok(new { result.Value.Token })
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapPost("users/register", [Authorize(Roles = nameof(UserRole.Admin))] 
                async (
                    [FromBody] CreateUserCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return result.IsSuccess
                        ? Results.Ok($"User created successfully with id: {result.Value.Id}!")
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapPatch("users/{id:int}", [Authorize(Roles = nameof(UserRole.Admin))] 
                async (
                   [FromRoute] int id, 
                   [FromBody] UpdateUserCommand command,
                   [FromServices] IMediator mediator,
                   [FromServices] CancellationToken ct) =>
                {
                    command = command with
                    {
                        UserId = id
                    };

                    var result = await mediator.Send(command, ct);

                    return result.IsSuccess
                        ? Results.Ok($"User with id {id} updated successfully!")
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapDelete("users/{id:int}", [Authorize(Roles = nameof(UserRole.Admin))] 
                async (
                    [FromRoute] int id,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new DeleteUserCommand(id), ct);

                    return result.IsSuccess
                        ? Results.Ok($"User with id {id} deleted successfully!")
                        : Results.BadRequest(result.ErrorMessage);
                });
        }

        public static void AddMangasEndpoints(this WebApplication app)
        {
            app.MapPost("mangas/register", [Authorize(Roles = nameof(UserRole.Admin))] 
                async (
                    [FromBody] CreateMangaCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return result.IsSuccess
                        ? Results.Ok($"Manga created successfully with id: {result.Value.Id}!")
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapPatch("mangas/{id:guid}", [Authorize(Roles = nameof(UserRole.Admin))] 
            async (
                    [FromRoute] Guid id,
                    [FromBody] UpdateMangaCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    command = command with
                    {
                        MangaId = id
                    };

                    var result = await mediator.Send(command, ct);

                    return result.IsSuccess
                        ? Results.Ok($"Manga with id {id} updated successfully!")
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapDelete("mangas/{id:guid}", [Authorize(Roles = nameof(UserRole.Admin))]
                async (
                        [FromRoute] Guid id,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new DeleteMangaCommand(id), ct);

                    return result.IsSuccess
                        ? Results.Ok($"Manga with id {id} deleted successfully!")
                        : Results.BadRequest(result.ErrorMessage);
                });
        }
        
        public static void AddOrdersEndpoints(this WebApplication app)
        {
            var ctSource = new CancellationTokenSource();

            app.MapGet("orders/{customerId:int}",
                async (
                        [FromRoute] int customerId,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetAllMangaOrderQuery(customerId), ct);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapGet("check-order/{id:ulid}",
                async (
                        [FromRoute] Ulid id,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetMangaOrderByIdQuery(id), ct);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.BadRequest(result.ErrorMessage);
                });

            app.MapPost("order",
                async (
                        [FromServices] CancellationToken ct) =>
                {
                    using var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(ctSource.Token, ct);
                });
        }
    }
}
