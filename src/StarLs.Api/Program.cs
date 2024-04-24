using StarLs.Api.Extensions;

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

app.Run();
