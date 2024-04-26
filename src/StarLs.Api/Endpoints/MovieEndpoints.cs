using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieRoutes(this WebApplication app)
        {
            app.MapGet("/movies", async ([FromServices] IHandler<GetMovieQueryRequest, List<GetMovieQueryResponse>> handler) =>
            {
                var result = await handler.Send(new GetMovieQueryRequest());
                return Results.Ok(result);
            })
            .WithTags("Movies");

            app.MapGet("/movies/{id}", async ([FromServices] IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                var result = await handler.Send(new GetMovieByIdQueryRequest(id));
                return Results.Ok(result);
            })
            .WithTags("Movies");
        }
    }
}
