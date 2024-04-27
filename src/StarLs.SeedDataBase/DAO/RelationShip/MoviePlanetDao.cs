using Dapper;
using Serilog;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.RelationShip;
internal class MoviePlanetDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(Dictionary<short, List<short>> MovieIds, Dictionary<short, List<short>> PlanetIds)
    {
        if (MovieIds.Count <= 0 || PlanetIds.Count <= 0)
        {
            _logger.Warning("list for relation is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO MoviePlanet (MoviesId, PlanetsId) VALUES ");
            queryBuiler.Append(@"(@MoviesId, @PlanetsId)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                foreach (var movieId in MovieIds)
                {
                    foreach (var planetId in movieId.Value)
                    {
                        await _conn.ExecuteAsync(
                                    queryBuiler.ToString(),
                                    new
                                    {
                                        MoviesId = movieId.Key,
                                        PlanetsId = planetId
                                    },
                                    transaction
                               );
                    }
                }

                await transaction.CommitAsync();
            }

            _logger.Information("Relationship between Movie and Planet was completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Movie Planet Dao {ex.Message}");
        }
    }
}
