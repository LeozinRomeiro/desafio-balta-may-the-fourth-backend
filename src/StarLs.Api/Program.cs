using StarLs.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDatabase();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
