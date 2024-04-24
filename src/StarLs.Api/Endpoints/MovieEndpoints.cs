using Microsoft.AspNetCore.Mvc;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Handlers.Interface;

namespace StarLs.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieRoutes(this WebApplication app)
        {
            app.MapGet("/movies", ([FromServices] IHandler<GetMovieQueryRequest, List<GetMovieQueryResponse>> handler) =>
            {
                return Results.Ok(handler.Send(new GetMovieQueryRequest()));
            });

            app.MapGet("/movies/{id}", ([FromServices] IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse> handler,[FromRoute] short id) =>
            {
                return Results.Ok(handler.Send(new GetMovieByIdQueryRequest(id)));
            });
        }
    }
}
