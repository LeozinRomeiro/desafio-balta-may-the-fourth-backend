using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StarLs.Application.Queries;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieRoutes(this WebApplication app)
        {
            app.MapGet("/movies", async ([FromServices] IHandler<GetMovieQueryRequest, List<GetMovieQueryResponse>> handler, [FromServices] IMemoryCache cache, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2) =>
            {
                var result = new QueryResult<GetMovieQueryResponse>(pageNumber, pageSize,
                    await cache.GetOrCreateAsync("MoviesCache", async item =>
                    {
                        item.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                        item.SlidingExpiration = TimeSpan.FromHours(12);

                        return await handler.Send(new GetMovieQueryRequest());
                    })
                    );
                
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
