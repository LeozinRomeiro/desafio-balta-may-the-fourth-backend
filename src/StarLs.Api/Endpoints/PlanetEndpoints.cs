using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Planets;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class PlanetEndpoints
    {
        public static void MapPlanetRoutes(this WebApplication app)
        {
            app.MapGet("/planets", ([FromServices] IHandler<GetPlanetQueryRequest, List<GetPlanetQueryResponse>> handler) =>
            {
                return Results.Ok(handler.Send(new GetPlanetQueryRequest()));
            });

            app.MapGet("/planets/{id}", ([FromServices] IHandler<GetPlanetByIdQueryRequest, GetPlanetByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                return Results.Ok(handler.Send(new GetPlanetByIdQueryRequest(id)));
            });
        }
    }
}
