using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Characters;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class CharacterEndpoints
    {
        public static void MapCharacterRoutes(this WebApplication app)
        {
            app.MapGet("/characters", ([FromServices] IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>> handler) =>
            {
                return Results.Ok(handler.Send(new GetCharacterQueryRequest()));
            })
            .WithTags("Character");

            app.MapGet("/characters/{id}", ([FromServices] IHandler<GetCharacterByIdQueryRequest, GetCharacterByIdQueryResponse> handler, [FromRoute] short id) =>
            {
                return Results.Ok(handler.Send(new GetCharacterByIdQueryRequest(id)));
            })
            .WithTags("Character");
        }
    }
}
