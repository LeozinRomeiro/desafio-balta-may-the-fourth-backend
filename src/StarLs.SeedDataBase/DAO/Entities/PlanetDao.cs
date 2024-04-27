using Dapper;
using Serilog;
using StarLs.Core.Entities;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.Entities;
internal class PlanetDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(List<Planet> planets)
    {
        if (planets.Count <= 0)
        {
            _logger.Warning("Planets list is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append("INSERT INTO Planet (Name, RotationPeriod, OrbitalPeriod, Diameter, Climate, Gravity, Terrain, SurfaceWater, Population) VALUES ");
            queryBuiler.Append("(@Title, @RotationPeriod, @OrbitalPeriod, @Diameter, @Climate, @Gravity, @Terrain, @SurfaceWater, @Population)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                for (int i = 0; i < planets.Count; i++)
                    await _conn.ExecuteAsync(queryBuiler.ToString(), planets[i], transaction);

                await transaction.CommitAsync();
            }

            _logger.Information("Planet table completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error planet dao {ex.Message}");
        }

    }
}
