using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoRabbitMQ.Models.Base;
using ProjetoRabbitMQ.Models.Enums;
using ProjetoRabbitMQ.Models.Login.Commands;
using ProjetoRabbitMQ.Models.Manga.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Commands;
using ProjetoRabbitMQ.Models.MangaOrder.Queries;
using ProjetoRabbitMQ.Models.User.Commands;

namespace ProjetoRabbitMQ.Controllers
{
    public static class ApiEndpoints
    {
        public static void AddEndpoints(this WebApplication app)
        {
            app.AddUsersEndpoints();
            app.AddMangasEndpoints();
            app.AddOrdersEndpoints();
        }

        private static void AddUsersEndpoints(this WebApplication app)
        {
            app.MapPost("users/login",
                async (
                    [FromBody] LoginCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return OkOrBadRequest(result);
                });

            app.MapPost("users/register", [Authorize(Roles = nameof(UserRole.Admin))]
                async (
                    [FromBody] CreateUserCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return OkOrBadRequest(result, $"User created successfully with id: {result.Value.Id}!");
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

                    return OkOrBadRequest(result, $"User with id {id} updated successfully!");
                });

            app.MapDelete("users/{id:int}", [Authorize(Roles = nameof(UserRole.Admin))]
                async (
                    [FromRoute] int id,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new DeleteUserCommand(id), ct);

                    return OkOrBadRequest(result, $"User with id {id} deleted successfully!");
                });
        }

        private static void AddMangasEndpoints(this WebApplication app)
        {
            app.MapPost("mangas/register", [Authorize(Roles = nameof(UserRole.Admin))]
                async (
                    [FromBody] CreateMangaCommand command,
                    [FromServices] IMediator mediator,
                    [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return OkOrBadRequest(result, $"Manga created successfully with id: {result.Value.Id}!");
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

                    return OkOrBadRequest(result, $"Manga with id {id} deleted successfully!");
                });
        }

        private static void AddOrdersEndpoints(this WebApplication app)
        {
            app.MapGet("orders/{customerId:int}",
                async (
                        [FromRoute] int customerId,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetAllMangaOrderQuery(customerId), ct);

                    return OkOrBadRequest(result);
                });

            app.MapGet("check-order/{id:ulid}",
                async (
                        [FromRoute] Ulid id,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetMangaOrderByIdQuery(id), ct);

                    return OkOrBadRequest(result);
                });

            app.MapPost("order",
                async (
                        [FromBody] RequestMangaOrderCommand command,
                        [FromServices] IMediator mediator,
                        [FromServices] CancellationToken ct) =>
                {
                    var result = await mediator.Send(command, ct);

                    return OkOrBadRequest(result, $"Order created successfully with id: {result.Value.MangaOrderId}!");
                });
        }

        private static IResult OkOrBadRequest<T>(Result<T> result, string? sucessMessage = null)
        {
            if (result.IsFailure)
            {
                return Results.BadRequest(result.ErrorMessage);
            }

            if (sucessMessage is not null)
            {
                return Results.Ok(sucessMessage);
            }

            return Results.Ok(result.Value);
        }
    }
}
