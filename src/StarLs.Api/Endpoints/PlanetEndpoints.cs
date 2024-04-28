using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries.Planets;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class PlanetEndpoints
    {
        public static void MapPlanetRoutes(this WebApplication app)
        {
            app.MapGet("/planets", async ([FromServices] IHandler<GetPlanetQueryRequest, List<GetPlanetQueryResponse>> handler, [FromServices] IMemoryCache cache) =>
            {
                var result = await cache.GetOrCreateAsync("PlanetsCache", async item =>
                {
                    item.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                    item.SlidingExpiration = TimeSpan.FromHours(12);

                    return await handler.Send(new GetPlanetQueryRequest());
                });
                
                return Results.Ok(result);
            })
            .WithTags("Planets");

            app.MapGet("/planets/{id}", async ([FromServices] IHandler<GetPlanetByIdQueryRequest, GetPlanetByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                var result = await handler.Send(new GetPlanetByIdQueryRequest(id));
                return Results.Ok(result);
            })
            .WithTags("Planets");
        }
    }
}
