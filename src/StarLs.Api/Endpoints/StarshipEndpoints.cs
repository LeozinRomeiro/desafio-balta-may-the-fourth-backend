using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Starships;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class StarshipEndpoints
    {
        public static void MapStarshipRoutes(this WebApplication app)
        {
            app.MapGet("/starships", async ([FromServices] IHandler<GetStarshipQueryRequest, List<GetStarshipQueryResponse>> handler) =>
            {
                var result = await handler.Send(new GetStarshipQueryRequest());
                return Results.Ok(result);
            })
            .WithTags("Starship");

            app.MapGet("/starships/{id}", async ([FromServices] IHandler<GetStarshipByIdQueryRequest, GetStarshipByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                var result = await handler.Send(new GetStarshipByIdQueryRequest(id));
                return Results.Ok(result);
            })
            .WithTags("Starship");
        }
    }
}
