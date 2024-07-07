using Dapper;
using Serilog;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.RelationShip;
internal class MovieVehicleDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(Dictionary<short, List<short>> MovieIds, Dictionary<short, List<short>> VehicleIds)
    {
        if (MovieIds.Count <= 0 || VehicleIds.Count <= 0)
        {
            _logger.Warning("list for relation is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO MovieVehicle (MoviesId, VehiclesId) VALUES ");
            queryBuiler.Append(@"(@MoviesId, @VehiclesId)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                foreach (var item in MovieIds)
                {
                    foreach (var vehicleId in item.Value)
                    {
                        await _conn.ExecuteAsync(
                                    queryBuiler.ToString(),
                                    new
                                    {
                                        MoviesId = item.Key,
                                        VehiclesId = vehicleId
                                    },
                                    transaction
                               );
                    }
                }

                await transaction.CommitAsync();
            }

            _logger.Information("Relationship between Movie and VehiclesId was completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Movie VehiclesId Dao {ex.Message}");
        }
    }
}
