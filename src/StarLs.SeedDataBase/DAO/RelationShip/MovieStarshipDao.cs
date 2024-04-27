using Dapper;
using Serilog;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.RelationShip;
internal class MovieStarshipDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(Dictionary<short, List<short>> MovieIds, Dictionary<short, List<short>> StarshipIds)
    {
        if (MovieIds.Count <= 0 || StarshipIds.Count <= 0)
        {
            _logger.Warning("list for relation is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO MovieStarship (MoviesId, StarshipsId) VALUES ");
            queryBuiler.Append(@"(@MoviesId, @StarshipsId)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                foreach (var movieId in MovieIds)
                {
                    foreach (var starShipId in movieId.Value)
                    {
                        await _conn.ExecuteAsync(
                                    queryBuiler.ToString(),
                                    new
                                    {
                                        MoviesId = movieId.Key,
                                        StarshipsId = starShipId
                                    },
                                    transaction
                               );
                    }
                }

                await transaction.CommitAsync();
            }

            _logger.Information("Relationship between Movie and Starship was completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Movie Starship Dao {ex.Message}");
        }
    }
}
