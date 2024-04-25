using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Starships;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class StarshipEndpoints
    {
        public static void MapStarshipRoutes(this WebApplication app)
        {
            app.MapGet("/starships", ([FromServices] IHandler<GetStarshipQueryRequest, List<GetStarshipQueryResponse>> handler) =>
            {
                return Results.Ok(handler.Send(new GetStarshipQueryRequest()));
            })
            .WithTags("Starship");

            app.MapGet("/starships/{id}", ([FromServices] IHandler<GetStarshipByIdQueryRequest, GetStarshipByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                return Results.Ok(handler.Send(new GetStarshipByIdQueryRequest(id)));
            })
            .WithTags("Starship");
        }
    }
}
