using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StarLs.SeedDataBase.DAO.Entities;
using StarLs.SeedDataBase.DAO.RelationShip;
using StarLs.SeedDataBase.Request;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .WriteTo.Console()
    .CreateLogger();

var serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .BuildServiceProvider();

var movieReq = new MovieRequest(serviceProvider.GetRequiredService<IHttpClientFactory>(), Log.Logger);
var movies = movieReq.Get();

var starShipReq = new StarshipsRequest(serviceProvider.GetRequiredService<IHttpClientFactory>(), Log.Logger);
var starShips = starShipReq.Get();

var planetReq = new PlanetRequest(serviceProvider.GetRequiredService<IHttpClientFactory>(), Log.Logger);
var planets = planetReq.Get();

var vehiclesReq = new VehicleRequest(serviceProvider.GetRequiredService<IHttpClientFactory>(), Log.Logger);
var vehicles = vehiclesReq.Get();

var characterReq = new CharacterRequest(serviceProvider.GetRequiredService<IHttpClientFactory>(), Log.Logger);
var characters = characterReq.Get();

//inser into entities tables
var movieDao = new MovieDao(Log.Logger);
movieDao.Create(movies);

var starshipDao = new StarshipDao(Log.Logger);
starshipDao.Create(starShips);

var planetDao = new PlanetDao(Log.Logger);
planetDao.Create(planets);

var vehicleDao = new VehicleDao(Log.Logger);
vehicleDao.Create(vehicles);

var characterDao = new CharacterDao(Log.Logger);
characterDao.Create(characters);

//insert into relationship tables
var characterMovieDao = new CharacterMovieDao(Log.Logger);
characterMovieDao.Create(characterReq.MoviesIds, movieReq.CharactersIds);

var moviePlanetDao = new MoviePlanetDao(Log.Logger);
moviePlanetDao.Create(planetReq.Movies, movieReq.PlanetsIds);

var movieStarshipDao = new MovieStarshipDao(Log.Logger);
movieStarshipDao.Create(starShipReq.MoviesIds, movieReq.StarshipsIds);

var movieVehicleDao = new MovieVehicleDao(Log.Logger);
movieVehicleDao.Create(vehiclesReq.MoviesIds, movieReq.VehiclesIds);

Log.Information("All done!");