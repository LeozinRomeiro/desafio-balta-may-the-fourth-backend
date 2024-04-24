using Microsoft.AspNetCore.Mvc;
using StarLs.Api.Endpoints;
using StarLs.Api.Extensions;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries.Movies;
using StarLs.Core.Entities;
using StarLs.Core.Handlers.Interface;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDatabase();

//add repositories
builder.ConfigureRepositories();

//add handlers
builder.ConfigureHandlers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapEndpoints();

app.Run();
