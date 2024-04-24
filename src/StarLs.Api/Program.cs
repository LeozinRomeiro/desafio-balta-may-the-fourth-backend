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

//add middleware exception
app.UseExceptionMiddleware();

app.MapGet("/", () => "Hello World!");

app.MapGet("/characters", async ([FromServices] IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>> handler) =>
{
    var result = await handler.Send(new GetCharacterQueryRequest());
    return Results.Ok(result);
});

// Altere a definição da rota /movies
app.MapGet("/movies", async ([FromServices] IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse> handler) =>
{
    var result = await handler.Send(new GetMovieByIdQueryRequest(1));
    return Results.Ok(result);
});


app.Run();
