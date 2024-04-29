using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries;
using StarLs.Core.Handlers.Interface;
using System.Diagnostics;

namespace StarLs.Api.Endpoints
{
    public static class CharacterEndpoints
    {
        public static void MapCharacterRoutes(this WebApplication app)
        {
            app.MapGet("/characters", async ([FromServices] IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>> handler, [FromServices] IMemoryCache cache, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25) =>
            {
                var result = new QueryResult<GetCharacterQueryResponse>(pageNumber, pageSize,
                    await cache.GetOrCreateAsync("CharactersCache", async item =>
                    {
                        item.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                        item.SlidingExpiration = TimeSpan.FromHours(12);

                        return await handler.Send(new GetCharacterQueryRequest());
                    })
                    );

                return Results.Ok(result);
            })
            .WithTags("Character");

            app.MapGet("/characters/{id}", async ([FromServices] IHandler<GetCharacterByIdQueryRequest, GetCharacterByIdQueryResponse> handler, [FromRoute] short id) =>
            {
                var result = await handler.Send(new GetCharacterByIdQueryRequest(id));
                return Results.Ok(result);
            })
            .WithTags("Character");
        }
    }
}
