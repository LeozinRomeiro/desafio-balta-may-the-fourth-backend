using Microsoft.EntityFrameworkCore;
using StarLs.Api.Endpoints;
using StarLs.Application.Mappings;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries.Movies;
using StarLs.Application.Queries.Planets;
using StarLs.Application.Queries.Starships;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Handlers.Interface;
using StarLs.Core.Repositories.Interfaces;
using StarLs.Infra.Context;
using StarLs.Infra.Repositories;

namespace StarLs.Api.Extensions;

public static class Configurations
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connString = "DataSource=app.db;Cache=Shared";


        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlite(connString, b => b.MigrationsAssembly("StarLs.Api"));
        });
    }

    public static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
        builder.Services.AddScoped<IStarshipRepository, StarshipRepository>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));        
    }

    public static void ConfigureHandlers(this WebApplicationBuilder builder)
    {
        //queries
        builder.Services.AddScoped<IHandler<GetCharacterQueryRequest, List<GetCharacterQueryResponse>>, GetCharacterQueryHandler>();
        builder.Services.AddScoped<IHandler<GetMovieQueryRequest, List<GetMovieQueryResponse>>, GetMovieQueryHandler>();
        builder.Services.AddScoped<IHandler<GetPlanetQueryRequest, List<GetPlanetQueryResponse>>, GetPlanetQueryHandler>();
        builder.Services.AddScoped<IHandler<GetStarshipQueryRequest, List<GetStarshipQueryResponse>>, GetStarshipQueryHandler>();
        builder.Services.AddScoped<IHandler<GetVehicleQueryRequest, List<GetVehicleQueryResponse>>, GetVehicleQueryHandler>();

        builder.Services.AddScoped<IHandler<GetCharacterByIdQueryRequest, GetCharacterByIdQueryResponse>, GetCharacterByIdQueryHandler>();
        builder.Services.AddScoped<IHandler<GetMovieByIdQueryRequest, GetMovieByIdQueryResponse>, GetMovieByIdQueryHandler>();
        builder.Services.AddScoped<IHandler<GetPlanetByIdQueryRequest, GetPlanetByIdQueryResponse>, GetPlanetByIdQueryHandler>();
        builder.Services.AddScoped<IHandler<GetStarshipByIdQueryRequest, GetStarshipByIdQueryResponse>, GetStarshipByIdQueryHandler>();
        builder.Services.AddScoped<IHandler<GetVehicleByIdQueryRequest, GetVehicleByIdQueryResponse>, GetVehicleByIdQueryHandler>();
    }

    public static void MapEndpoints(this WebApplication app)
    {
        app.MapCharacterRoutes();
        app.MapMovieRoutes();
        app.MapPlanetRoutes();
        app.MapStarshipRoutes();
        app.MapVehicleRoutes();
    }
}
