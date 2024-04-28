using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieRoutes(this WebApplication app)
        {
            app.MapGet("/movies", async ([FromServices] IHandler<GetMovieQueryRequest, List<GetMovieQueryResponse>> handler, [FromServices] IMemoryCache cache) =>
            {
                var memoryCache = cache.GetOrCreate("MoviesCache", item =>
                {
                    item.SlidingExpiration = TimeSpan.FromHours(1);
                    return DateTime.Now;
                });
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
