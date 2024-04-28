using StarLs.Api.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Starls - BackendTeam",
        Description = "Developed by: Anthony D | Kirmct N | Leonardo R | Rodolfo J | Victor A",
        Contact = new OpenApiContact { Url = new Uri("https://github.com/lrodolfol/desafio-balta-may-the-fourth-backend") },
        License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

builder.ConfigureDatabase();
builder.ConfigureRepositories();
builder.ConfigureHandlers();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapEndpoints();
app.UseExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();
