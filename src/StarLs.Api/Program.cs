using Microsoft.AspNetCore.Mvc;
using StarLs.Api.Extensions;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Handlers.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDatabase();

//add repositories
builder.ConfigureRepositories();

//add handlers
builder.ConfigureHandlers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/characters", ([FromServices] IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>> handler) =>
{
    return Results.Ok(handler.Send(new GetCharacterQueryRequest()));
});

app.MapGet("/movies", ([FromServices] IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse> handler) =>
{
    return Results.Ok(handler.Send(new GetMovieByIdQueryRequest(1)));
});


app.Run();
